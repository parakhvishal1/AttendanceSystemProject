using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AttendanceSystemDemo.Contracts;
using AttendanceSystemDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemDemo.Controllers
{
    [Route("api/attendance")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly IAttendanceRepository _attendanceRepository;
        public AttendanceController(IAttendanceRepository attendanceRepository)
        {
            _attendanceRepository = attendanceRepository;
        }
        private async Task<bool> AttendanceExist(int id)
        {
            return await _attendanceRepository.Exist(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<Attendance>))]

        public IActionResult GetAttendance([FromRoute] int Id)
        {
            var results = new ObjectResult(_attendanceRepository.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };
            Request.HttpContext.Response.Headers.Add("X-Total-Count", _attendanceRepository.GetAll().Count().ToString());
            return results;
        }


        [HttpGet("{id}")]
        [Produces(typeof(Attendance))]
        public async Task<IActionResult> GetAttendances([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var attendance = await _attendanceRepository.Find(id);

            if (attendance == null)
            {
                return NotFound();
            }

            return Ok(attendance);
        }

        [HttpGet("{id}/{start}/{end}/{present}")]
        [Produces(typeof(Attendance))]
        public IActionResult FindAttendanceByDate([FromRoute] int id, [FromRoute] string start, [FromRoute] string end, [FromRoute] string present)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var results =  _attendanceRepository.FindAttendanceByDate(id, start, end, present);

            if (results == null)
            {
                return NotFound();
            }
            

            return Ok(results);

        }

            [HttpPut("{Id}")]
        [Produces(typeof(Attendance))]
        public async Task<IActionResult> PutAttendance([FromRoute] int id, [FromBody] Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != attendance.Id)
            {
                return BadRequest();
            }

            try
            {
                await _attendanceRepository.Update(attendance);
                return Ok(attendance);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await AttendanceExist(id))
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
        [Produces(typeof(Attendance))]
        public async Task<IActionResult> PostAttendance([FromBody] Attendance attendance)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _attendanceRepository.Add(attendance);

            return CreatedAtAction("GetAttendance", new { id = attendance.Id }, attendance);
        }


        [HttpDelete("{id}")]
        [Produces(typeof(Attendance))]
        public async Task<IActionResult> DeleteAttendance([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await AttendanceExist(id))
            {
                return NotFound();
            }

            await _attendanceRepository.Remove(id);

            return Ok();

        }

    }
}
