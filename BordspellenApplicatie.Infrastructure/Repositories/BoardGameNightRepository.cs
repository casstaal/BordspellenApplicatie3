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

        public void Delete(BoardGameNight boardGameNight)
        {
            dBContext.BoardGameNights.Remove(boardGameNight);
            dBContext.SaveChanges();
        }

        public IEnumerable<BoardGameNight> GetAll()
        {
            return dBContext.BoardGameNights
                .Include(bgn => bgn.UserBoardGameNights)
                .Include(bgn => bgn.Address)
                .Include(bgn => bgn.Games)
                .ToList();
        }

        public BoardGameNight? GetById(int id)
        {
            return dBContext.BoardGameNights
            .Include(bg => bg.Organizer)
            .Include(bg => bg.UserBoardGameNights)
            .ThenInclude(ugn => ugn.User)
            .Include(bg => bg.Games)
            .FirstOrDefault(bg => bg.Id == id);
        }

        public IEnumerable<User> GetRegisteredUsers(BoardGameNight boardGameNight)
        {
            throw new NotImplementedException();
        }

        public void Update(BoardGameNight boardGameNight)
        {
            dBContext.Update(boardGameNight);
            dBContext.SaveChanges();
        }

        public IEnumerable<Boardgame> GetAllGames(BoardGameNight boardGameNight)
        {
            throw new NotImplementedException();
        }
    }
}
