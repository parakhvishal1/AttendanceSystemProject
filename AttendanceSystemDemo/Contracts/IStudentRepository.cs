using System;
using AttendanceSystemDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystemDemo.Contracts
{
   public interface IStudentRepository
    {
        Task<Student> Add(Student Student);

        IEnumerable<Student> GetAll();

        Task<Student> Find(int id);

        Task<Student> Update(Student Student);

        Task<Student> Remove(int id);

        Task<bool> Exist(int id);

    }
}
