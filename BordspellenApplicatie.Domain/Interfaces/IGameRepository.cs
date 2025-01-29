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
        public Boardgame GetById(Guid id);
        public void Add(Boardgame boardgame);
        public void Update(Boardgame boardgame);
        public void Delete(Guid id);
    }
}
