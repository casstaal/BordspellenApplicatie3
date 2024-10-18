using BordspellenApplicatie.Domain.Interfaces;
using BordspellenApplicatie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Infrastructure.Repositories
{
    public class GameRepository : IGameRepository
    {
        public Task<Boardgame> AddAsync(Boardgame game)
        {
            throw new NotImplementedException();
        }

        public Task<Boardgame> DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Boardgame> GetAll()
        {
            throw new NotImplementedException();
        }

        public IQueryable<Boardgame> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Boardgame GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Boardgame> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<Boardgame> UpdateAsync(Boardgame game)
        {
            throw new NotImplementedException();
        }
    }
}
