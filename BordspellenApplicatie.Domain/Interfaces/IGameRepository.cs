using BordspellenApplicatie.Models;
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
        public void GetById(int id);
        public void Add();
    }
}
