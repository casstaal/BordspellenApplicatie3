using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Infrastructure.Repositories
{
    public class GameRepository(BordspellenavondDBContext dBContext) : IGameRepository
    {
        public async Task<Boardgame> Add(Boardgame boardgame)
        {
            await dBContext.Games.AddAsync(boardgame);
            await dBContext.SaveChangesAsync();
            return boardgame;
        }

        public async Task<Boardgame?> Delete(Guid id)
        {
            var existingBoardgame = await dBContext.Games.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBoardgame == null)
            {
                return null;
            }

            dBContext.Games.Remove(existingBoardgame);
            await dBContext.SaveChangesAsync();
            return existingBoardgame;
        }

        public async Task<List<Boardgame>> GetAll()
        {
            return await dBContext.Games.ToListAsync();
        }

        public async Task<List<Boardgame>?> GetByBoardGameNightId(int id)
        {
            return await dBContext.Games.Where(x => x.BoardGameNightId == id).ToListAsync();
        }

        public async Task<Boardgame?> GetById(Guid id)
        {
            return await dBContext.Games.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Boardgame?> Update(Guid id, Boardgame boardgame)
        {
            var existingBoardgame = await dBContext.Games.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBoardgame == null)
            {
                return null;
            }

            existingBoardgame.Name = boardgame.Name;
            existingBoardgame.Description = boardgame.Description;
            existingBoardgame.Genre = boardgame.Genre;
            existingBoardgame.Is18Plus = boardgame.Is18Plus;
            existingBoardgame.ImageUrl = boardgame.ImageUrl;
            existingBoardgame.Type = boardgame.Type;

            await dBContext.SaveChangesAsync();

            return existingBoardgame;
        }
    }
}
