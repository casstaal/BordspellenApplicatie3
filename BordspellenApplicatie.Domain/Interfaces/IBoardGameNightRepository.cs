using BordspellenApplicatie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Domain.Interfaces
{
    public interface IBoardGameNightRepository
    {
        Task<List<BoardGameNight>> GetAll();
        Task<BoardGameNight?> GetById(int id);
        Task<BoardGameNight> Add(BoardGameNight boardGameNight);
        Task<BoardGameNight?> Update(int id, BoardGameNight boardGameNight);
        Task<bool> SetIs18Plus(int id);
        Task<BoardGameNight?> Delete(int id);

        public IEnumerable<Boardgame> GetAllGames(BoardGameNight boardGameNight);

        public IEnumerable<User> GetRegisteredUsers(BoardGameNight boardGameNight);
        Task<int> CountByOrganizerIdAsync(int organizerId);
    }
}
