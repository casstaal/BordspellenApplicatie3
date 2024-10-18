using BordspellenApplicatie.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BordspellenApplicatie.Infrastructure.Data;
using BordspellenApplicatie.Domain.Models;


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
            throw new NotImplementedException();
        }

        public void Update(User user)
        {
            throw new NotImplementedException();
        }
    }
}
