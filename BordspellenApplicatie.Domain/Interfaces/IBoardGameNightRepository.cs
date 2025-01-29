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
        public IEnumerable<BoardGameNight> GetAll();

        public BoardGameNight? GetById(int id);
        public void Add(BoardGameNight boardGameNight);
        public void Update(BoardGameNight boardGameNight);
        public void Delete(int id);
        void Delete(BoardGameNight boardGameNight);

        public IEnumerable<Boardgame> GetAllGames(BoardGameNight boardGameNight);

        public IEnumerable<User> GetRegisteredUsers(BoardGameNight boardGameNight);
    }
}
