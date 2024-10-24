using BordspellenApplicatie.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BordspellenApplicatie.Infrastructure.Data;
using BordspellenApplicatie.Domain.Models;
using Microsoft.EntityFrameworkCore;


namespace BordspellenApplicatie.Infrastructure.Repositories
{
    public class UserRepository(BordspellenavondDBContext dBContext) : IUserRepository
    {
        public void Add(User user)
        {
            dBContext.Add(user);
            dBContext.SaveChanges();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAll()
        {
            throw new NotImplementedException();
        }

        public User GetById(int id)
        {
            return dBContext.Users.Find(id);
        }

        public User? GetByIdentityId(string identityId)
        {
            return dBContext.Users.FirstOrDefault(u => u.IdentityId == identityId);
        }

        public void Update(User user)
        {
            dBContext.Update(user);
            dBContext.SaveChanges();
        }

        public async Task UpdateAsync(User user)
        {
            var userToUpdate = await dBContext.Users
                //.Include(u => u.UserBoardGameNights)  // Load the collection
                .Include(bgn => bgn.Address)
                .FirstOrDefaultAsync(bgn => bgn.Id == user.Id);

            if (userToUpdate == null)
            {
                throw new InvalidOperationException("User not found");
            }

            userToUpdate.FirstName = user.FirstName;
            userToUpdate.LastName = user.LastName;
            userToUpdate.DateOfBirth = user.DateOfBirth;
            userToUpdate.Address.Street = user.Address.Street;
            userToUpdate.Address.Number = user.Address.Number;
            userToUpdate.Address.City = user.Address.City;
            userToUpdate.UserBoardGameNights = user.UserBoardGameNights;

            await dBContext.SaveChangesAsync();
        }

        public async Task<User> GetUserByIdentityIdAsync(string identityId)
        {
            return await dBContext.Users.SingleOrDefaultAsync(u => u.IdentityId == identityId);
        }

        public IEnumerable<BoardGameNight> getOrganizedBoardGameNights(string identityID)
        {
            var user = dBContext.Users.FirstOrDefault(u => u.IdentityId == identityID);

            if (user == null)
            {
                return Enumerable.Empty<BoardGameNight>(); 
            }

            return dBContext.BoardGameNights
                .Include(bg => bg.Organizer)
                .Include(bg => bg.Games) 
                .Where(bg => bg.Organizer.Id == user.Id) 
                .ToList(); 
        }
    }
}
