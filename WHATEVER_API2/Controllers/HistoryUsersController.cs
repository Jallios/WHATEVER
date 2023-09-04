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
    public class HistoryUsersController : ControllerBase
    {
        private readonly WHATEVERContext _context;

        public HistoryUsersController(WHATEVERContext context)
        {
            _context = context;
        }

        // GET: api/HistoryUsers
        [HttpGet]
        public async Task<ActionResult<IEnumerable<HistoryUser>>> GetHistoryUsers()
        {
            return await _context.HistoryUsers.ToListAsync();
        }

        // GET: api/HistoryUsers/5
        [HttpGet("{id}")]
        public async Task<ActionResult<HistoryUser>> GetHistoryUser(int? id)
        {
            var historyUser = await _context.HistoryUsers.FindAsync(id);

            if (historyUser == null)
            {
                return NotFound();
            }

            return historyUser;
        }

        // PUT: api/HistoryUsers/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHistoryUser(int? id, HistoryUser historyUser)
        {
            if (id != historyUser.IdHistoryUser)
            {
                return BadRequest();
            }

            _context.Entry(historyUser).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HistoryUserExists(id))
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

        // POST: api/HistoryUsers
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<HistoryUser>> PostHistoryUser(HistoryUser historyUser)
        {
            _context.HistoryUsers.Add(historyUser);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHistoryUser", new { id = historyUser.IdHistoryUser }, historyUser);
        }

        // DELETE: api/HistoryUsers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHistoryUser(int? id)
        {
            var historyUser = await _context.HistoryUsers.FindAsync(id);
            if (historyUser == null)
            {
                return NotFound();
            }

            _context.HistoryUsers.Remove(historyUser);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool HistoryUserExists(int? id)
        {
            return _context.HistoryUsers.Any(e => e.IdHistoryUser == id);
        }
    }
}
