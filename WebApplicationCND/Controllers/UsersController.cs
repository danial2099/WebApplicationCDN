using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplicationCND.Models;


namespace WebApplicationCND.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserContext _userContext;
        public UsersController(UserContext userContext)
        {
            _userContext = userContext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            if (_userContext.Users == null)
            {
                return NotFound();
            }
            return await _userContext.Users.ToListAsync();
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            if (_userContext.Users == null)
            {
                return NotFound();
            }
            var user = await _userContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _userContext.Users.Add(user);
            await _userContext.SaveChangesAsync();

            return CreatedAtAction(nameof(GetUser), new { id = user.ID }, user);
        }

        [HttpPut]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if(id != user.ID)
            {
                return BadRequest();
            }
            _userContext.Entry(user).State = EntityState.Modified;

            try
            {
                await _userContext.SaveChangesAsync();
            }
            catch(DbUpdateConcurrencyException)
            {
                if (!UserStatus(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return Ok();
        }
        private bool UserStatus(int id)
        {
            return (_userContext.Users?.Any(x => x.ID == id)).GetValueOrDefault();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (_userContext.Users == null)
            {
                return NotFound();
            }
            var user = await _userContext.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _userContext.Users.Remove(user);
            await _userContext.SaveChangesAsync();
            
            return Ok();

        }
    }
}
