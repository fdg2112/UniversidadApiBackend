using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UniversidadApiBackend.DataAccess;
using UniversidadApiBackend.Helpers;
using UniversidadApiBackend.Models.DataModels;

namespace UniversidadApiBackend.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UniversityDBContext _context;

        private readonly JwtSettings _jwtSettings;

        public AccountController(UniversityDBContext context, JwtSettings jwtSettings)
        {
            _context = context;
            _jwtSettings = jwtSettings;
        }

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();

                var searchedUser = (from user in _context.Users
                                    where user.Name == userLogin.UserName && user.Password == userLogin.Password
                                    select user).FirstOrDefault();

                if (searchedUser != null)
                {
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        Id = searchedUser.Id,
                        UserName = searchedUser.Name,
                        EmailId = searchedUser.EmailAddress,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                }
                else
                {
                    return BadRequest("Wrong Credentials");
                }
                return Ok(Token);
            }
            catch (Exception ex)
            {
                throw new Exception("GetToken Error", ex);
            }
        }

        [HttpGet]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<IEnumerable<User>>> GetUserList()
        {
            return await _context.Users.ToListAsync();
        }

    }
}
