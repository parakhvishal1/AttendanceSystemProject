using System;
using AttendanceSystemDemo.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceSystemDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemDemo.Repositories
{
    public class AttendanceRepository : IAttendanceRepository
    {
        private AttendanceContext _context;
        public AttendanceRepository(AttendanceContext context)
        {
            _context =context;
        }
        public async Task<Attendance> Add(Attendance Attendance)
        {
            await _context.Attendance.AddAsync(Attendance);
            return Attendance;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Attendance.AnyAsync(c => c.Id == id);
        }

        public async Task<Attendance> Find(int id)
        {
            return await _context.Attendance.Include(Attendance => Attendance.Id).SingleOrDefaultAsync(a => a.Id == id);
        }


        public   IEnumerable<Attendance> FindAttendanceByDate(int id, string start, string end, string present)
        {

           

            var attendance = _context.Attendance.Where(a => a.Id == id && a.Date == start && a.Date == end && a.Status == present).ToList();
            return attendance;
            
            

        }

        public IEnumerable<Attendance> GetAll()
        {
            return _context.Attendance;
        }



        public async Task<Attendance> Remove(int id)
        {
            var attendance = await _context.Attendance.SingleAsync(a => a.Id == id);
            _context.Attendance.Remove(attendance);
            await _context.SaveChangesAsync();
            return attendance;
        }

        public async Task<Attendance> Update(Attendance Attendance)
        {
            _context.Attendance.Update(Attendance);
            await _context.SaveChangesAsync();
            return Attendance;

        }
    }
}
