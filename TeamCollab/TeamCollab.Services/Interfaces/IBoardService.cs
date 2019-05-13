﻿using System.Collections.Generic;
using System.Threading.Tasks;
using TeamCollab.Data.Models;

namespace TeamCollab.Services.Interfaces
{
    public interface IBoardService
    {
        Task<Board> AddBoardAsync(int projectId, string name);
        Task EditBoardAsync(int boardId, string name);
        Task DeleteBoardAsync(int boardId);
        Task AddCardToBoardAsync(int boardId, string content, string userId);
        Task EditCardAsync(int cardId, string content, string userId);
        Task MoveCardAsync(int cardId, int? prevCardId, int? nextCardId, string userId);
        Task DeleteCardAsync(int cardId);
        Task<IEnumerable<Card>> GetCardsAsync(int boardId);
    }
}
