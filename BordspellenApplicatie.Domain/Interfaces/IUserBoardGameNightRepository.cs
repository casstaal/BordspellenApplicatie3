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
        public IEnumerable<UserBoardGameNight> GetAll(string userId);

        // Gebruik voor inschrijven user
        Task AddUserBoardGameNightAsync(UserBoardGameNight userBoardGameNight);

        //Gebruik voor uitschrijven user
        Task DeleteUserBoardGameNightAsync(UserBoardGameNight userBoardGameNight);


        Task<bool> IsUserRegisteredForBoardGameNightAsync(int boardGameNightId, string userId);
    }
}
