using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WHATEVER_API2.Models;

namespace WHATEVER_API2.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusArticlesController : ControllerBase
    {
        private readonly WHATEVERContext _context;

        public StatusArticlesController(WHATEVERContext context)
        {
            _context = context;
        }

        // GET: api/StatusArticles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StatusArticle>>> GetStatusArticles()
        {
            return await _context.StatusArticles.ToListAsync();
        }

        // GET: api/StatusArticles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StatusArticle>> GetStatusArticle(int? id)
        {
            var statusArticle = await _context.StatusArticles.FindAsync(id);

            if (statusArticle == null)
            {
                return NotFound();
            }

            return statusArticle;
        }

        // PUT: api/StatusArticles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStatusArticle(int? id, StatusArticle statusArticle)
        {
            if (id != statusArticle.IdStatusArticle)
            {
                return BadRequest();
            }

            _context.Entry(statusArticle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StatusArticleExists(id))
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

        // POST: api/StatusArticles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StatusArticle>> PostStatusArticle(StatusArticle statusArticle)
        {
            _context.StatusArticles.Add(statusArticle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetStatusArticle", new { id = statusArticle.IdStatusArticle }, statusArticle);
        }

        // DELETE: api/StatusArticles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStatusArticle(int? id)
        {
            var statusArticle = await _context.StatusArticles.FindAsync(id);
            if (statusArticle == null)
            {
                return NotFound();
            }

            _context.StatusArticles.Remove(statusArticle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StatusArticleExists(int? id)
        {
            return _context.StatusArticles.Any(e => e.IdStatusArticle == id);
        }
    }
}