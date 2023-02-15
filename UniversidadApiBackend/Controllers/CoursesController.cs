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
using UniversidadApiBackend.Services;

namespace UniversidadApiBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CoursesController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly ILogger<CoursesController> _logger;
        private readonly ICoursesService _coursesService;

        public CoursesController(UniversityDBContext context, ICoursesService coursesService, ILogger<CoursesController> logger)
        {
            _context = context;
            _coursesService = coursesService;
            _logger = logger;
        }

        // GET: api/Courses
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Course>>> GetCourses()
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(GetCourses)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(GetCourses)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(GetCourses)} - Critical Level Log");
            return await _context.Courses.ToListAsync();
        }

        // GET: api/Courses/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Course>> GetCourse(int id)
        {
            var course = await _context.Courses.FindAsync(id);

            if (course == null)
            {
                return NotFound();
            }

            return course;
        }

        // PUT: api/Courses/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutCourse(int id, Course course)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(PutCourse)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(PutCourse)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(PutCourse)} - Critical Level Log");
            if (id != course.Id)
            {
                return BadRequest();
            }

            _context.Entry(course).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CourseExists(id))
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

        // POST: api/Courses
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Course>> PostCourse(Course course)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(PostCourse)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(PostCourse)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(PostCourse)} - Critical Level Log");
            _context.Courses.Add(course);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCourse", new { id = course.Id }, course);
        }

        // DELETE: api/Courses/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteCourse(int id)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(DeleteCourse)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(DeleteCourse)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(DeleteCourse)} - Critical Level Log");
            var course = await _context.Courses.FindAsync(id);
            if (course == null)
            {
                return NotFound();
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CourseExists(int id)
        {
            return _context.Courses.Any(e => e.Id == id);
        }
    }
}
