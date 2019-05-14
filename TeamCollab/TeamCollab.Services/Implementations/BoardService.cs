using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using TeamCollab.Data;
using TeamCollab.Data.Models;
using TeamCollab.Services.Interfaces;

namespace TeamCollab.Services.Implementations
{
    public class BoardService : IBoardService
    {
        private readonly TeamCollabDbContext db;

        public BoardService(TeamCollabDbContext db)
        {
            this.db = db;
        }


        public IQueryable<Board> GetBoards(int projectId)
        {
            return this.db.Boards.Where(b => b.ProjectId == projectId);
        }

        public async Task<Board> AddBoardAsync(int projectId, string name)
        {
            var board = new Board()
            {
                Name = name,
                ProjectId = projectId
            };

            await this.db.Boards.AddAsync(board);

            await this.db.SaveChangesAsync();

            return board;
        }

        public async Task EditBoardAsync(int boardId, string name)
        {
            var board = await this.db.Boards.FindAsync(boardId);

            board.Name = name;

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteBoardAsync(int boardId)
        {
            var board = await this.db.Boards.FindAsync(boardId);

            this.db.Boards.Remove(board);

            await this.db.SaveChangesAsync();
        }

        public async Task AddCardToBoardAsync(int boardId, string content, string userId)
        {
            var board = await this.db.Boards.FindAsync(boardId);

            var card = new Card()
            {
                Board = board,
                Content = content,
                Created = DateTime.Now,
                LastModified = DateTime.Now,
                LastModifiedById = userId
            };

            if (board.RootCardId.HasValue)
            {
                var last = await this.db.Cards.FindAsync(board.RootCardId);
                while (last.NextCardId.HasValue)
                {
                    last = await this.db.Cards.FindAsync(board.RootCardId);
                }

                last.Next = card;
            }
            else
            {
                board.Root = card;
            }

            await this.db.SaveChangesAsync();
        }

        public async Task EditCardAsync(int cardId, string content, string userId)
        {
            var card = await this.db.Cards.FindAsync(cardId);

            card.Content = content;
            card.LastModifiedById = userId;
            card.LastModified = DateTime.Now;

            await this.db.SaveChangesAsync();
        }

        public async Task MoveCardAsync(int cardId, int? prevCardId, int? nextCardId, string userId)
        {
            var card = await this.db.Cards.Include(c => c.Next).Include(c => c.Prev).FirstAsync(c => c.Id == cardId);

            if (card.Prev != null)
            {
                card.Prev.Next = card.Next;
            }
            if (card.Next != null)
            {
                card.Next.Prev = card.Prev;
            }

            if (nextCardId.HasValue)
            {
                var next = await this.db.Cards.Include(c => c.Next).Include(c => c.Prev).FirstAsync(c => c.Id == nextCardId);

                next.Prev = card;
            }
            card.NextCardId = nextCardId;

            if (prevCardId.HasValue)
            {
                var prev = await this.db.Cards.Include(c => c.Next).Include(c => c.Prev).FirstAsync(c => c.Id == prevCardId);

                prev.Next = card;
            }
            card.PrevCardId = prevCardId;

            await this.db.SaveChangesAsync();
        }

        public async Task DeleteCardAsync(int cardId)
        {
            var card = await this.db.Cards.Include(c => c.Next).Include(c => c.Prev).FirstAsync(c => c.Id == cardId);

            this.db.Cards.Remove(card);

            await this.db.SaveChangesAsync();
        }

        public async Task<IEnumerable<Card>> GetCardsAsync(int boardId)
        {
            var board = await this.db.Boards.Include(b => b.Cards).FirstAsync(b => b.Id == boardId);

            var cards = board.Cards.ToDictionary(c => c.Id, c => c);

            var result = new List<Card>();

            var first = board.RootCardId;
            while (first.HasValue)
            {
                result.Add(cards[first.Value]);
                first = cards[first.Value].NextCardId;
            }

            return result;
        }
    }
}
