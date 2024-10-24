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
            //return dBContext.UserBoardGameNights.ToList();

            return dBContext.UserBoardGameNights
                .Where(ubgn => ubgn.UserId == userId)  // Filter by userId
                .Include(ubgn => ubgn.GameNight)  // Assuming you want details of the BoardGameNight too
                .ThenInclude(gn => gn.Organizer)       // Include the Organizer of the GameNight
                .ToList();
        }

        public async Task AddUserBoardGameNightAsync(UserBoardGameNight userBoardGameNight)
        {
            dBContext.UserBoardGameNights.AddAsync(userBoardGameNight);
            await dBContext.SaveChangesAsync();
        }

        public async Task DeleteUserBoardGameNightAsync(UserBoardGameNight userBoardGameNight)
        {
            // Find the existing entry for the given user and board game night
            var existingEntry = await dBContext.UserBoardGameNights
                .FirstOrDefaultAsync(ubgn => ubgn.UserId == userBoardGameNight.UserId && ubgn.BoardGameNightId == userBoardGameNight.BoardGameNightId);

            if (existingEntry != null)
            {
                // Remove the entry from the context
                dBContext.UserBoardGameNights.Remove(existingEntry);

                // Save changes to persist the deletion
                await dBContext.SaveChangesAsync();
            }
        }


        public async Task<bool> IsUserRegisteredForBoardGameNightAsync(int boardGameNightId, string userId)
        {
            return await dBContext.UserBoardGameNights
                .AnyAsync(ubgn => ubgn.BoardGameNightId == boardGameNightId && ubgn.UserId.ToString() == userId);
        }

        public async Task DeleteUserGameNightAsync(int gamenightId, int userId)
        {
            // Find the existing entry for the given user and board game night
            var existingEntry = await dBContext.UserBoardGameNights
                .FirstOrDefaultAsync(ubgn => ubgn.UserId == userId && ubgn.BoardGameNightId == gamenightId);

            if (existingEntry != null)
            {
                // Remove the entry from the context
                dBContext.UserBoardGameNights.Remove(existingEntry);

                // Save changes to persist the deletion
                await dBContext.SaveChangesAsync();
            }
        }
    }
}
