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
        public void Add(Boardgame boardgame)
        {
            throw new NotImplementedException();
        }

        public void Delete(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Boardgame> GetAll()
        {
            throw new NotImplementedException();
        }

        public Boardgame GetById(Guid id)
        {
            throw new NotImplementedException();
        }

        public void Update(Boardgame boardgame)
        {
            throw new NotImplementedException();
        }
    }
}
