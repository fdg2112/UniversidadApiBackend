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
    public class StudentsController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly ILogger<StudentsController> _logger;
        private readonly IStudentsService _studentService;

        public StudentsController(UniversityDBContext context, IStudentsService studentService, ILogger<StudentsController> logger)
        {
            _context = context;
            _studentService = studentService;
            _logger = logger;
        }

        // GET: api/Students
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Student>>> Getstudents()
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(Getstudents)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(Getstudents)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(Getstudents)} - Critical Level Log");
            return await _context.students.ToListAsync();
        }

        // GET: api/Students/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Student>> GetStudent(int id)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(GetStudent)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(GetStudent)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(GetStudent)} - Critical Level Log");
            var student = await _context.students.FindAsync(id);

            if (student == null)
            {
                return NotFound();
            }

            return student;
        }

        // PUT: api/Students/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutStudent(int id, Student student)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(PutStudent)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(PutStudent)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(PutStudent)} - Critical Level Log");
            if (id != student.Id)
            {
                return BadRequest();
            }

            _context.Entry(student).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentExists(id))
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

        // POST: api/Students
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Student>> PostStudent(Student student)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(PostStudent)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(PostStudent)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(PostStudent)} - Critical Level Log");
            _context.students.Add(student);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStudent", new { id = student.Id }, student);
        }

        // DELETE: api/Students/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteStudent(int id)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(DeleteStudent)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(DeleteStudent)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(DeleteStudent)} - Critical Level Log");
            var student = await _context.students.FindAsync(id);
            if (student == null)
            {
                return NotFound();
            }

            _context.students.Remove(student);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentExists(int id)
        {
            return _context.students.Any(e => e.Id == id);
        }
    }
}
