using System;
using AttendanceSystemDemo.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceSystemDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemDemo.Repositories
{
    public class TeacherRepository : ITeacherRepository
    {
        private AttendanceContext _context;
        public TeacherRepository(AttendanceContext context)
        {
            _context = context;
        }
        public async Task<Teacher> Add(Teacher Teacher)
        {
            await _context.Teacher.AddAsync(Teacher);
            return Teacher;
        }

        public async Task<bool> Exist(int id)
        {
            return await  _context.Teacher.AnyAsync(c => c.Id == id);
        }

        public async Task<Teacher> Find(int id)
        {
            return await _context.Teacher.Include(Teacher => Teacher.Id).SingleOrDefaultAsync(a => a.Id == id);
        }

        public  IEnumerable<Teacher> GetAll()
        {
            return _context.Teacher;
        }

        public async Task<Teacher> Remove(int id)
        {
            var teacher = await _context.Teacher.SingleAsync(a => a.Id == id);
            _context.Teacher.Remove(teacher);
            await _context.SaveChangesAsync();
            return teacher;
        }

        public async Task<Teacher> Update(Teacher Teacher)
        {
            _context.Teacher.Update(Teacher);
            await _context.SaveChangesAsync();
            return Teacher;
        }
    }
}
