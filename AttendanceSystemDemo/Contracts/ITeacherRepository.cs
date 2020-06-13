using System;
using AttendanceSystemDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystemDemo.Contracts
{
    public interface ITeacherRepository
    {
        Task<Teacher> Add(Teacher Teacher);

        IEnumerable<Teacher> GetAll();

        Task<Teacher> Find(int id);

        Task<Teacher> Update(Teacher Teacher);

        Task<Teacher> Remove(int id);

        Task<bool> Exist(int id);

    }
}
