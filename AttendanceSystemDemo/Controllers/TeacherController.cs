using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AttendanceSystemDemo.Contracts;
using AttendanceSystemDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemDemo.Controllers
{
    [Route("api/teacher")]
    [ApiController]
    public class TeacherController : ControllerBase
    {
        private readonly ITeacherRepository _teacherRepository;
        public TeacherController(ITeacherRepository teacherRepository)
        {
            _teacherRepository = teacherRepository;
        }
        private async Task<bool> TeacherExist(int id)
        {
            return await _teacherRepository.Exist(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Teacher>))]

        public IActionResult GetTeaher([FromRoute] int Id)
        {
            var results = new ObjectResult(_teacherRepository.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };
            Request.HttpContext.Response.Headers.Add("X-Total-Count", _teacherRepository.GetAll().Count().ToString());
            return results;
        }


        [HttpGet("{id}")]
        [Produces(typeof(Teacher))]
        public async Task<IActionResult> GetTeacher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var teacher = await _teacherRepository.Find(id);

            if (teacher == null)
            {
                return NotFound();
            }

            return Ok(teacher);
        }

        [HttpPut("{Id}")]
        [Produces(typeof(Teacher))]
        public async Task<IActionResult> PutTeacher([FromRoute] int id, [FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != teacher.Id)
            {
                return BadRequest();
            }

            try
            {
                await _teacherRepository.Update(teacher);
                return Ok(teacher);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await TeacherExist(id))
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
        [Produces(typeof(Teacher))]
        public async Task<IActionResult> PostTeacher([FromBody] Teacher teacher)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _teacherRepository.Add(teacher);

            return CreatedAtAction("GetTeacher", new { id = teacher.Id }, teacher);
        }


        [HttpDelete("{id}")]
        [Produces(typeof(Teacher))]
        public async Task<IActionResult> DeleteTeacher([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await TeacherExist(id))
            {
                return NotFound();
            }

            await _teacherRepository.Remove(id);

            return Ok();
        }


    }
}
    
