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
    public class UserBoardGameNightRepository(BordspellenavondDBContext dBContext) : IUserBoardGameNightRepository
    {
        public IEnumerable<UserBoardGameNight> GetAll(int userId)
        {

            return dBContext.UserBoardGameNights
                .Where(ubgn => ubgn.UserId == userId)  
                .Include(ubgn => ubgn.GameNight)  
                .ThenInclude(gn => gn.Organizer)   
                .ToList();
        }

        public async Task AddUserBoardGameNightAsync(UserBoardGameNight userBoardGameNight)
        {
            dBContext.UserBoardGameNights.AddAsync(userBoardGameNight);
            await dBContext.SaveChangesAsync();
        }

        public async Task DeleteUserBoardGameNightAsync(UserBoardGameNight userBoardGameNight)
        {
            var existingEntry = await dBContext.UserBoardGameNights
                .FirstOrDefaultAsync(ubgn => ubgn.UserId == userBoardGameNight.UserId && ubgn.BoardGameNightId == userBoardGameNight.BoardGameNightId);

            if (existingEntry != null)
            {
                dBContext.UserBoardGameNights.Remove(existingEntry);

                await dBContext.SaveChangesAsync();
            }
        }


        public async Task<bool> IsUserRegisteredForBoardGameNightAsync(int boardGameNightId, int userId)
        {
            return await dBContext.UserBoardGameNights
                .AnyAsync(ubgn => ubgn.BoardGameNightId == boardGameNightId && ubgn.UserId == userId);
        }

        public async Task DeleteUserGameNightAsync(int gamenightId, int userId)
        {
            var existingEntry = await dBContext.UserBoardGameNights
                .FirstOrDefaultAsync(ubgn => ubgn.UserId == userId && ubgn.BoardGameNightId == gamenightId);

            if (existingEntry != null)
            {
                dBContext.UserBoardGameNights.Remove(existingEntry);

                await dBContext.SaveChangesAsync();
            }
        }
    }
}
