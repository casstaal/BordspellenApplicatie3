using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using BordspellenApplicatie.Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Infrastructure.Repositories
{
    public class BoardGameNightRepository(BordspellenavondDBContext dBContext) : IBoardGameNightRepository
    {
        public void Add(BoardGameNight boardGameNight)
        {
            dBContext.Add(boardGameNight);
            dBContext.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BoardGameNight> GetAll()
        {
            throw new NotImplementedException();
        }

        public BoardGameNight GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetRegisteredUsers(BoardGameNight boardGameNight)
        {
            throw new NotImplementedException();
        }

        public void Update(BoardGameNight boardGameNight)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Boardgame> GetAllGames(BoardGameNight boardGameNight)
        {
            throw new NotImplementedException();
        }
    }
}
