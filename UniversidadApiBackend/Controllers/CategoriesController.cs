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
    public class CategoriesController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly ILogger<CategoriesController> _logger;
        private readonly ICategoriesService _categoriesService;

        public CategoriesController(UniversityDBContext context, ICategoriesService categoriesService, ILogger<CategoriesController> logger)
        {
            _context = context;
            _categoriesService = categoriesService;
            _logger = logger;
        }

        // GET: api/Categories
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Category>>> GetCategories()
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(GetCategories)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(GetCategories)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(GetCategories)} - Critical Level Log");

            return await _context.Categories.ToListAsync();
        }

        // GET: api/Categories/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Category>> GetCategory(int id)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(GetCategory)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(GetCategory)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(GetCategory)} - Critical Level Log");
            var category = await _context.Categories.FindAsync(id);

            if (category == null)
            {
                return NotFound();
            }

            return category;
        }

        // PUT: api/Categories/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutCategory(int id, Category category)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(PutCategory)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(PutCategory)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(PutCategory)} - Critical Level Log");

            if (id != category.Id)
            {
                return BadRequest();
            }

            _context.Entry(category).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CategoryExists(id))
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

        // POST: api/Categories
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Category>> PostCategory(Category category)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(PostCategory)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(PostCategory)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(PostCategory)} - Critical Level Log");
            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCategory", new { id = category.Id }, category);
        }

        // DELETE: api/Categories/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(DeleteCategory)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(DeleteCategory)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(DeleteCategory)} - Critical Level Log");
            var category = await _context.Categories.FindAsync(id);
            if (category == null)
            {
                return NotFound();
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CategoryExists(int id)
        {
            return _context.Categories.Any(e => e.Id == id);
        }
    }
}
