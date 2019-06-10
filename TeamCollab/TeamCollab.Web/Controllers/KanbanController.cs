using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamCollab.Data.Enums;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;
using TeamCollab.Web.Models.ArchiveViewModels;
using TeamCollab.Web.Models.KanbanViewModels;

namespace TeamCollab.Web.Controllers
{
    [Authorize]
    public class KanbanController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly IBoardService boardService;
        private readonly IProjectService projectService;
        private readonly ILogService logService;

        public KanbanController(UserManager<User> userManager, IBoardService boardService, IProjectService projectService, ILogService logService)
        {
            this.userManager = userManager;
            this.boardService = boardService;
            this.projectService = projectService;
            this.logService = logService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
            if (!await this.projectService.IsWorkerInProjectAsync(id, this.User.Identity.Name))
            {
                return this.Unauthorized();
            }

            this.ViewData["projectId"] = id;

            var boards = await this.boardService.GetBoards(id).ToListAsync();

            var model = new List<BoardViewModel>();

            foreach (var board in boards)
            {
                var cards = await this.boardService.GetCardsAsync(board.Id);

                model.Add(new BoardViewModel()
                {
                    Name = board.Name,
                    Id = board.Id,
                    Cards = cards.AsQueryable().ProjectTo<CardViewModel>().ToList()
                });
            }

            return this.View(model);
        }

        public async Task<IActionResult> AddBoard(string name, int projectId)
        {
            var board = await this.boardService.AddBoardAsync(projectId, name);

            return this.Json(board);
        }

        public async Task<IActionResult> AddCard(int projectId, int boardId, string text)
        {
            await this.boardService.AddCardToBoardAsync(boardId, text, this.userManager.GetUserId(this.User));

            await this.logService.CreateAsync(this.userManager.GetUserId(this.User), projectId, $"{this.User.Identity.Name} created \"{text}\"", EventType.Success);

            return this.RedirectToAction("Index", new { id = projectId });
        }

        public async Task<IActionResult> MoveCard(int cardId, int boardId, int? prevCardId, int? nextCardId)
        {
            await this.boardService.MoveCardAsync(cardId, boardId, prevCardId, nextCardId,
                this.userManager.GetUserId(this.User));

            var card = await this.boardService.GetCardAsync(cardId);

            await this.logService.CreateAsync(this.userManager.GetUserId(this.User), card.Board.ProjectId, $"{this.User.Identity.Name} moved \"{card.Content}\" to \"{card.Board.Name}\"", EventType.Success);

            return this.Ok();
        }

        public async Task<IActionResult> DeleteBoard(int boardId)
        {
            await this.boardService.DeleteBoardAsync(boardId);

            return this.Ok();
        }

        public async Task<IActionResult> DeleteCard(int cardId)
        {
            await this.boardService.DeleteCardAsync(cardId);

            var card = await this.boardService.GetCardAsync(cardId);

            await this.logService.CreateAsync(this.userManager.GetUserId(this.User), card.Board.ProjectId, $"{this.User.Identity.Name} deleted \"{card.Content}\"", EventType.Danger);

            return this.Ok();
        }

        public async Task<IActionResult> ArchiveCard(int cardId)
        {
            await this.boardService.ArchiveCardAsync(cardId);

            var card = await this.boardService.GetCardAsync(cardId);

            await this.logService.CreateAsync(this.userManager.GetUserId(this.User), card.Board.ProjectId, $"{this.User.Identity.Name} archived \"{card.Content}\"", EventType.Warning);

            return this.Ok();
        }

        public IActionResult Archived(int id)
        {
            var archived = this.boardService.GetArchived(id);

            var model = archived.ProjectTo<ArchivedListViewModel>().ToList();

            return this.View(model);
        }
    }
}
