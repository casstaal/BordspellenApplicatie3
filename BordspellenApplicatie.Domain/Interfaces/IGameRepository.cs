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
        Task<List<Boardgame>> GetAll();
        Task<Boardgame?> GetById(Guid id);
        Task<List<Boardgame>?> GetByBoardGameNightId(int id);
        Task<Boardgame> Add(Boardgame boardgame);
        Task<Boardgame?> Update(Guid id, Boardgame boardgame);
        Task<Boardgame?> Delete(Guid id);
    }
}
