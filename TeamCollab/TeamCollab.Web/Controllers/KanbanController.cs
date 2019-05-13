using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;

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
        public IActionResult Index(int id)
        {
            this.ViewData["projectId"] = id;

            return this.View();
        }

        public async Task<IActionResult> AddBoard(string name, int projectId)
        {
            var board = await this.boardService.AddBoardAsync(projectId, name);

            return this.Json(board);
        }
    }
}
