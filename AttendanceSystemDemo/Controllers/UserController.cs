using System.Linq;
using System.Net;
using System.Threading.Tasks;
using AttendanceSystemDemo.Contracts;
using AttendanceSystemDemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AttendanceSystemDemo.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        private async Task<bool> UserExist(int id)
        {
            return await _userRepository.Exist(id);
        }

        [HttpGet]
        [Produces(typeof(DbSet<User>))]
        
        public  IActionResult GetUsers([FromRoute] int Id)
        {
            var results = new ObjectResult(_userRepository.GetAll())
            {
                StatusCode = (int)HttpStatusCode.OK
            };
            Request.HttpContext.Response.Headers.Add("X-Total-Count", _userRepository.GetAll().Count().ToString());

            return results;
        }

       
        [HttpGet("{id}")]
        [Produces(typeof(DbSet<User>))]
        public async Task<IActionResult> GetUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _userRepository.GetUser(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPut("{Id}")]
        [Produces(typeof(User))]
        public async Task<IActionResult> PutUser([FromRoute] int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != user.Id)
            {
                return BadRequest();
            }

            try
            {
                await _userRepository.Update(user);
                return Ok(user);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await UserExist(id))
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
        [Produces(typeof(User))]
        public async Task<IActionResult> PostUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _userRepository.Add(user);

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }


        [HttpDelete("{id}")]
        [Produces(typeof(User))]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (!await UserExist(id))
            {
                return NotFound();
            }

            await _userRepository.Remove(id);

            return Ok();
        }

    }
}
