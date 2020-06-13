using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AttendanceSystemDemo.Contracts;
using AttendanceSystemDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemDemo.Controllers
{
    [Route("api/student")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly IStudentRepository _studentRepository;
        public StudentController(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        private async Task<bool> StudentExist(int id)
        {
            return await _studentRepository.Exist(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Student>))]

        public IActionResult GetStudent([FromRoute] int Id)
        {
            var results = new ObjectResult(_studentRepository.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };
            Request.HttpContext.Response.Headers.Add("X-Total-Count", _studentRepository.GetAll().Count().ToString());
            return results;
        }


        [HttpGet("{id}")]
        [Produces(typeof(Student))]
        public async  Task<IActionResult> GetStudents([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var student = await _studentRepository.Find(id);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPut("{Id}")]
        [Produces(typeof(Student))]
        public async Task<IActionResult> PutStudent([FromRoute] int id, [FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != student.Id)
            {
                return BadRequest();
            }

            try
            {
                await _studentRepository.Update(student);
                return Ok(student);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await StudentExist(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

        }
        [HttpPost]
        [Produces(typeof(Student))]
        public async Task<IActionResult> PostStudent([FromBody] Student student)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _studentRepository.Add(student);

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }


        [HttpDelete("{id}")]
        [Produces(typeof(Student))]
        public async Task<IActionResult> DeleteStudent([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await StudentExist(id))
            {
                return NotFound();
            }

            await _studentRepository.Remove(id);

            return Ok();

        }


    }
}
    

