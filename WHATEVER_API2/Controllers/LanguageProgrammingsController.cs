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
    public class LanguageProgrammingsController : ControllerBase
    {
        private readonly WHATEVERContext _context;

        public LanguageProgrammingsController(WHATEVERContext context)
        {
            _context = context;
        }

        // GET: api/LanguageProgrammings
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LanguageProgramming>>> GetLanguageProgrammings()
        {
            return await _context.LanguageProgrammings.ToListAsync();
        }

        // GET: api/LanguageProgrammings/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LanguageProgramming>> GetLanguageProgramming(int? id)
        {
            var languageProgramming = await _context.LanguageProgrammings.FindAsync(id);

            if (languageProgramming == null)
            {
                return NotFound();
            }

            return languageProgramming;
        }

        // PUT: api/LanguageProgrammings/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutLanguageProgramming(int? id, LanguageProgramming languageProgramming)
        {
            if (id != languageProgramming.IdLanguageProgramming)
            {
                return BadRequest();
            }

            _context.Entry(languageProgramming).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LanguageProgrammingExists(id))
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

        // POST: api/LanguageProgrammings
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<LanguageProgramming>> PostLanguageProgramming(LanguageProgramming languageProgramming)
        {
            _context.LanguageProgrammings.Add(languageProgramming);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetLanguageProgramming", new { id = languageProgramming.IdLanguageProgramming }, languageProgramming);
        }

        // DELETE: api/LanguageProgrammings/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteLanguageProgramming(int? id)
        {
            var languageProgramming = await _context.LanguageProgrammings.FindAsync(id);
            if (languageProgramming == null)
            {
                return NotFound();
            }

            _context.LanguageProgrammings.Remove(languageProgramming);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool LanguageProgrammingExists(int? id)
        {
            return _context.LanguageProgrammings.Any(e => e.IdLanguageProgramming == id);
        }
    }
}
