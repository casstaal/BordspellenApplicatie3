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
    public class BoardGameNightRepository(BordspellenavondDBContext dBContext) : IBoardGameNightRepository
    {
        public async Task<BoardGameNight> Add(BoardGameNight boardGameNight)
        {
            await dBContext.BoardGameNights.AddAsync(boardGameNight);
            await dBContext.SaveChangesAsync();
            return boardGameNight;
        }

        public async Task<BoardGameNight?> Delete(int id)
        {
            var existingBoardGameNight = await dBContext.BoardGameNights.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBoardGameNight == null)
            {
                return null;
            }

            dBContext.BoardGameNights.Remove(existingBoardGameNight);
            await dBContext.SaveChangesAsync();
            return existingBoardGameNight;
        }

        public async Task<List<BoardGameNight>> GetAll()
        {
            return await dBContext.BoardGameNights
                .Include(bgn => bgn.UserBoardGameNights)
                .Include(bgn => bgn.Address)
                .Include(bgn => bgn.Games)
                .ToListAsync();
        }

        public async Task<BoardGameNight?> GetById(int id)
        {
            return await dBContext.BoardGameNights
            .Include(bg => bg.Organizer)
            .Include(bg => bg.UserBoardGameNights)
            .ThenInclude(ugn => ugn.User)
            .Include(bg => bg.Games)
            .FirstOrDefaultAsync(bg => bg.Id == id);
        }

        public IEnumerable<User> GetRegisteredUsers(BoardGameNight boardGameNight)
        {
            throw new NotImplementedException();
        }

        public async Task<BoardGameNight?> Update(int id, BoardGameNight boardGameNight)
        {
            var existingBoardGameNight = await dBContext.BoardGameNights.FirstOrDefaultAsync(x => x.Id == id);

            if (existingBoardGameNight == null)
            {
                return null;
            }

            existingBoardGameNight.DateTime = boardGameNight.DateTime;
            existingBoardGameNight.Address = boardGameNight.Address;
            existingBoardGameNight.Is18Plus = boardGameNight.Is18Plus;
            existingBoardGameNight.Title = boardGameNight.Title;
            existingBoardGameNight.ImageUrl = boardGameNight.ImageUrl;
            existingBoardGameNight.Description = boardGameNight.Description;
            existingBoardGameNight.MaxPlayers = boardGameNight.MaxPlayers;
            existingBoardGameNight.Food = boardGameNight.Food;
            existingBoardGameNight.IsLactoseIntoleranceFriendly = boardGameNight.IsLactoseIntoleranceFriendly;
            existingBoardGameNight.IsVegetarianFriendly = boardGameNight.IsVegetarianFriendly;
            existingBoardGameNight.IsNutAllergyFriendly = boardGameNight.IsNutAllergyFriendly;
            existingBoardGameNight.IsNonAlcoholicFriendly = boardGameNight.IsNonAlcoholicFriendly;

            //await dBContext.Update(boardGameNight);
            await dBContext.SaveChangesAsync();

            return existingBoardGameNight;
        }

        public async Task<bool> SetIs18Plus(int id)
        {
            var gameNight = await dBContext.BoardGameNights
                .FirstOrDefaultAsync(x => x.Id == id);

            if (gameNight == null)
            {
                return false;
            }

            gameNight.Is18Plus = true;

            await dBContext.SaveChangesAsync();

            return true;
        }

        public IEnumerable<Boardgame> GetAllGames(BoardGameNight boardGameNight)
        {
            throw new NotImplementedException();
        }

        public async Task<int> CountByOrganizerIdAsync(int organizerId)
        {
            return await dBContext.BoardGameNights
                .CountAsync(x => x.OrganizerId == organizerId);
        }
    }
}
