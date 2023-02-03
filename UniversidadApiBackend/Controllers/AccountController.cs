using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
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
            _jwtSettings= jwtSettings;
        }


        // TO DO: cambiar por usuarios reales
        //private IEnumerable<User> Logins = new List<User>()
        //{
        //    new User()
        //    {
        //        Id= 1,
        //        EmailAddress = "fdg@gmail.com",
        //        Name = "Admin",
        //        Password = "admin"
        //    },
        //    new User()
        //    {
        //        Id= 2,
        //        EmailAddress = "2112@gmail.com",
        //        Name = "User1",
        //        Password = "user1"
        //    }
        //};

        [HttpPost]
        public IActionResult GetToken(UserLogins userLogin)
        {
            try
            {
                var Token = new UserTokens();

                var searchedUser = (from user in _context.Users
                                    where user.Name == userLogin.UserName && user.Password == userLogin.Password
                                    select user).FirstOrDefault();

                //var Valid = Logins.Any(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                if (searchedUser != null)
                {
                    //var user = Logins.FirstOrDefault(user => user.Name.Equals(userLogin.UserName, StringComparison.OrdinalIgnoreCase));
                    Token = JwtHelpers.GenTokenKey(new UserTokens()
                    {
                        Id = searchedUser.Id,
                        UserName = searchedUser.Name,
                        EmailId = searchedUser.EmailAddress,
                        GuidId = Guid.NewGuid()
                    }, _jwtSettings);
                } else
                {
                    return BadRequest("Wrong Credentials");
                }
                return Ok(Token);
            } catch(Exception ex)
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
