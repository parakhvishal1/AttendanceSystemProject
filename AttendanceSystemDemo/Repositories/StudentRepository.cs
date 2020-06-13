using System;
using AttendanceSystemDemo.Contracts;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AttendanceSystemDemo.Models;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemDemo.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private AttendanceContext _context;
        public StudentRepository(AttendanceContext context)
        {
            _context = context;
        }
        public async Task<Student> Add(Student Student)
        {
            await _context.Student.AddAsync(Student);
            return Student;
        }

        public async Task<bool> Exist(int id)
        {
            return await _context.Student.AnyAsync(c => c.Id == id);
        }

        public async Task<Student> Find(int id)
        {
            return await _context.Student.Include(Student => Student.Id).SingleOrDefaultAsync(a => a.Id == id);
        }

        public IEnumerable<Student> GetAll()
        {
            return _context.Student;
        }

        public async Task<Student> Remove(int id)
        {
            var student = await _context.Student.SingleAsync(a => a.Id == id);
            _context.Student.Remove(student);
            await _context.SaveChangesAsync();
            return student;
        }

        public async Task<Student> Update(Student Student)
        {
            _context.Student.Update(Student);
            await _context.SaveChangesAsync();
            return Student;
        }
    }
}
