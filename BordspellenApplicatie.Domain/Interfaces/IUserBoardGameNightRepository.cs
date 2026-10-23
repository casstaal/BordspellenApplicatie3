using BordspellenApplicatie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Domain.Interfaces
{
    public interface IUserBoardGameNightRepository
    {
        public IEnumerable<UserBoardGameNight> GetAll(int userId);
        Task AddUserBoardGameNightAsync(UserBoardGameNight userBoardGameNight);
        Task DeleteUserBoardGameNightAsync(UserBoardGameNight userBoardGameNight);

        Task<bool> IsUserRegisteredForDateAsync(int userId, DateTime date);
        Task<bool> IsUserRegisteredForBoardGameNightAsync(int boardGameNightId, int userId);
        public Task DeleteUserGameNightAsync(int gamenightId, int userId);

    }
}
