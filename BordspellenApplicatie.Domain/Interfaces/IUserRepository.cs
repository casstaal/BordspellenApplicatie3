using BordspellenApplicatie.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BordspellenApplicatie.Domain.Interfaces
{
    public interface IUserRepository
    {
        public IEnumerable<User> GetAll();
        public IQueryable<User> GetAllAsync();

        public User GetById(int id);
        public Task<User> GetByIdAsync(int id);

        public Task<User> AddAsync(User user);
        public Task<User> UpdateAsync(User user);
        public Task<User> DeleteAsync(int id);
    }
}
