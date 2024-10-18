using BordspellenApplicatie.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Domain.Interfaces
{
    public interface IGameRepository
    {
        public IEnumerable<Boardgame> GetAll();
        public IQueryable<Boardgame> GetAllAsync();

        public Boardgame GetById(Guid id);
        public Task<Boardgame> GetByIdAsync(Guid id);

        public Task<Boardgame> AddAsync(Boardgame game);
        public Task<Boardgame> UpdateAsync(Boardgame game);
        public Task<Boardgame> DeleteAsync(int id);
    }
}
