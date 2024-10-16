using BordspellenApplicatie.Models;
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
        public IQueryable<BoardGameNight> GetAllAsync();

        public BoardGameNight GetById(int id);
        public Task<BoardGameNight> GetByIdAsync(int id);

        public Task<BoardGameNight> AddAsync(BoardGameNight boardGameNight);
        public Task<BoardGameNight> UpdateAsync(BoardGameNight boardGameNight);
        public Task<BoardGameNight> DeleteAsync(int id);

        // Nog een methode om de bijbehorende games op te vragen

        // Nog een methode om de ingeschreven mensen op te vragen

        // Nog een methode om de organisator op te vragen

        // Nog een methode om het eten op te vragen
    }
}
