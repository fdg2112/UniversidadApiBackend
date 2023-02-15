using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversidadApiBackend.DataAccess;
using UniversidadApiBackend.Models.DataModels;
using UniversityApiBackend.Controllers;

namespace UniversidadApiBackend.Controllers
{
    [Route("api/[controller]")] // seria localhost:7291/api/Users
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly ILogger<UsersController> _logger;
        public UsersController(UniversityDBContext context, ILogger<UsersController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(GetUsers)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(GetUsers)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(GetUsers)} - Critical Level Log");

            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(GetUser)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(GetUser)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(GetUser)} - Critical Level Log");

            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(PutUser)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(PutUser)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(PutUser)} - Critical Level Log");

            if (id != user.Id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(PostUser)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(PostUser)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(PostUser)} - Critical Level Log");

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(DeleteUser)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(DeleteUser)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(DeleteUser)} - Critical Level Log");

            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
