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
    public class ChaptersController : ControllerBase
    {
        private readonly UniversityDBContext _context;
        private readonly ILogger<ChaptersController> _logger;
        private readonly IChaptersService _chaptersService;

        public ChaptersController(UniversityDBContext context, IChaptersService chaptersService, ILogger<ChaptersController> logger)
        {
            _context = context;
            _chaptersService = chaptersService;
            _logger = logger;
        }

        // GET: api/Chapters
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Chapter>>> GetChapters()
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(GetChapters)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(GetChapters)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(GetChapters)} - Critical Level Log");
            return await _context.Chapters.ToListAsync();
        }

        // GET: api/Chapters/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Chapter>> GetChapter(int id)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(GetChapters)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(GetChapters)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(GetChapters)} - Critical Level Log");
            var chapter = await _context.Chapters.FindAsync(id);

            if (chapter == null)
            {
                return NotFound();
            }

            return chapter;
        }

        // PUT: api/Chapters/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> PutChapter(int id, Chapter chapter)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(PutChapter)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(PutChapter)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(PutChapter)} - Critical Level Log");
            if (id != chapter.Id)
            {
                return BadRequest();
            }

            _context.Entry(chapter).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ChapterExists(id))
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

        // POST: api/Chapters
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<ActionResult<Chapter>> PostChapter(Chapter chapter)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(PostChapter)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(PostChapter)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(PostChapter)} - Critical Level Log");
            _context.Chapters.Add(chapter);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetChapter", new { id = chapter.Id }, chapter);
        }

        // DELETE: api/Chapters/5
        [HttpDelete("{id}")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme, Roles = "Administrator")]
        public async Task<IActionResult> DeleteChapter(int id)
        {
            _logger.LogWarning($"{nameof(UsersController)} - {nameof(DeleteChapter)} - Warning Level Log");
            _logger.LogError($"{nameof(UsersController)} - {nameof(DeleteChapter)} - Error Level Log");
            _logger.LogCritical($"{nameof(UsersController)} - {nameof(DeleteChapter)} - Critical Level Log");
            var chapter = await _context.Chapters.FindAsync(id);
            if (chapter == null)
            {
                return NotFound();
            }

            _context.Chapters.Remove(chapter);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ChapterExists(int id)
        {
            return _context.Chapters.Any(e => e.Id == id);
        }
    }
}
