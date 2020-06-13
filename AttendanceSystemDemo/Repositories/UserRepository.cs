using System;
using AttendanceSystemDemo.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceSystemDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemDemo.Repositories
{
    public class UserRepository : IUserRepository
    {
        private AttendanceContext _context;
        public UserRepository(AttendanceContext context)
        {
            _context = context;
        }
        public async Task<User> Add(User User)
        {
            await _context.User.AddAsync(User);
            return User;
          

        }

        public async Task<bool> Exist(int id)
        {
            return await _context.User.AnyAsync(c => c.Id == id);
        }

        public async Task<User> GetUser(int id)
        {
            return await _context.User.Include(user => user.Id).SingleOrDefaultAsync(a => a.Id == id);
        }

        public IEnumerable<User> GetAll()
        {
            return _context.User;
        }

        public async Task<User> Remove(int id)
        {
            var user = await _context.User.SingleAsync(a => a.Id == id);
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task<User> Update(User User)
        {
            _context.User.Update(User);
            await _context.SaveChangesAsync();
            return User;
        }
    }
}
