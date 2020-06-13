using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceSystemDemo.Models;

namespace AttendanceSystemDemo.Contracts
{
    public interface IUserRepository
    {
        Task<User> Add(User User);

        IEnumerable<User> GetAll();

        Task<User> GetUser(int id);

        Task<User> Update(User User);

        Task<User> Remove(int id);

        Task<bool> Exist(int id);
    }
}
