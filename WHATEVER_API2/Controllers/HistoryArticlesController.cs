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
    public class HistoryArticlesController : ControllerBase
    {
        private readonly WHATEVERContext _context;

        public HistoryArticlesController(WHATEVERContext context)
        {
            _context = context;
        }

        // GET: api/HistoryArticles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryArticle>>> GetHistoryArticles()
        {
            return await _context.HistoryArticles.ToListAsync();
        }

        // GET: api/HistoryArticles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoryArticle>> GetHistoryArticle(int? id)
        {
            var historyArticle = await _context.HistoryArticles.FindAsync(id);

            if (historyArticle == null)
            {
                return NotFound();
            }

            return historyArticle;
        }

        // PUT: api/HistoryArticles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoryArticle(int? id, HistoryArticle historyArticle)
        {
            if (id != historyArticle.IdHistoryArticle)
            {
                return BadRequest();
            }

            _context.Entry(historyArticle).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryArticleExists(id))
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

        // POST: api/HistoryArticles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistoryArticle>> PostHistoryArticle(HistoryArticle historyArticle)
        {
            _context.HistoryArticles.Add(historyArticle);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistoryArticle", new { id = historyArticle.IdHistoryArticle }, historyArticle);
        }

        // DELETE: api/HistoryArticles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryArticle(int? id)
        {
            var historyArticle = await _context.HistoryArticles.FindAsync(id);
            if (historyArticle == null)
            {
                return NotFound();
            }

            _context.HistoryArticles.Remove(historyArticle);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoryArticleExists(int? id)
        {
            return _context.HistoryArticles.Any(e => e.IdHistoryArticle == id);
        }
    }
}
