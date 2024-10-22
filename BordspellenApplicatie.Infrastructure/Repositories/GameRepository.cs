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
    public class GameRepository(BordspellenavondDBContext dBContext) : IGameRepository
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
            return dBContext.Games.ToList();
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
