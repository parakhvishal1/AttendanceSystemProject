using System;
using AttendanceSystemDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AttendanceSystemDemo.Contracts
{
    public  interface IAttendanceRepository
    {
        Task<Attendance> Add(Attendance Attendance);

        IEnumerable<Attendance> GetAll();

        Task<Attendance> Find(int id);

        Task<Attendance> Update(Attendance Attendance);

        Task<Attendance> Remove(int id);
        IEnumerable<Attendance> FindAttendanceByDate(int id, string start,string end, string present);



        Task<bool> Exist(int id);
    }
}
