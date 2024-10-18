using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Infrastructure.Repositories
{
    public class BoardGameNightRepository : IBoardGameNightRepository
    {
        public Task<BoardGameNight> AddAsync(BoardGameNight boardGameNight)
        {
            throw new NotImplementedException();
        }

        public Task<BoardGameNight> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<BoardGameNight> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<BoardGameNight> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public BoardGameNight GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BoardGameNight> GetByIdAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task<BoardGameNight> UpdateAsync(BoardGameNight boardGameNight)
        {
            throw new NotImplementedException();
        }
    }
}
