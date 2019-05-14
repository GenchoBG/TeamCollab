using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;
using TeamCollab.Web.Models.KanbanViewModels;

namespace TeamCollab.Web.Controllers
{
    [Authorize]
    public class KanbanController : Controller
    {
        private UserManager<User> userManager;
        private readonly IBoardService boardService;

        public KanbanController(UserManager<User> userManager, IBoardService boardService)
        {
            this.userManager = userManager;
            this.boardService = boardService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(int id)
        {
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
    }
}
