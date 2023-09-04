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
    public class ArticlesController : ControllerBase
    {
        private readonly WHATEVERContext _context;

        public ArticlesController(WHATEVERContext context)
        {
            _context = context;
        }

        // GET: api/Articles
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticles(string? header, int? languageId)
        {

            if (header == "" && languageId == 0)
            {
                return await _context.Articles.Where(x => x.StatusArticleId == 2).Include(x => x.user).Include(x => x.language).ToListAsync();
            }
            if (string.IsNullOrEmpty(header) && languageId == null)
            {
                return await _context.Articles.Where(x => x.StatusArticleId == 2).Include(x => x.user).Include(x => x.language).ToListAsync();
            }
            else if (string.IsNullOrEmpty(header) && languageId != null)
            {
                return await _context.Articles.Where(x => x.StatusArticleId == 2).Where(x => x.LanguageProgrammingId == languageId).Include(x => x.user).Include(x => x.language).ToListAsync();
            }
            else if (!string.IsNullOrEmpty(header) && languageId == null)
            {
                return await _context.Articles.Where(x => x.StatusArticleId == 2).Where(x => x.Header.Contains(header)).Include(x => x.user).Include(x => x.language).ToListAsync();
            }
            else
            {
                return await _context.Articles.Where(x => x.StatusArticleId == 2).Where(x => x.LanguageProgrammingId == languageId && x.Header.Contains(header)).Include(x => x.user).Include(x => x.language).ToListAsync();
            }


        }

        [HttpGet("language")]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticlesLanguage(int? languageId)
        {
            var al = await _context.Articles.Where(x => x.StatusArticleId == 2).Where(x => x.LanguageProgrammingId == languageId).Include(x => x.user).Include(x => x.language).ToListAsync();

            if (al == null)
            {
                return NotFound();
            }

            return al;

        }

        [HttpGet("status")]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticlesStatus()
        {
            var al = await _context.Articles.Where(x => x.StatusArticleId == 1).Include(x => x.user).Include(x => x.language).Include(x => x.statusArticle).ToListAsync();

            if (al == null)
            {
                return NotFound();
            }

            return al;

        }

        [HttpGet("userID")]
        public async Task<ActionResult<IEnumerable<Article>>> GetArticlesUser(int? uid)
        {
            var al = await _context.Articles.Where(x => x.UserId == uid).Where(x => x.StatusArticleId != 3).Include(x => x.user).Include(x => x.language).Include(x => x.statusArticle).ToListAsync();

            if (al == null)
            {
                return NotFound();
            }

            return al;

        }

        // GET: api/Articles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Article>> GetArticle(int? id)
        {
            var article = await _context.Articles.Include(x => x.statusArticle).Include(x => x.user).Include(x => x.language).Where(x => x.IdArticle == id).FirstOrDefaultAsync();

            if (article == null)
            {
                return NotFound();
            }

            return article;
        }

        // PUT: api/Articles/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutArticle(int? id, Article article)
        {
            if (id != article.IdArticle)
            {
                return BadRequest();
            }

            _context.Entry(article).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ArticleExists(id))
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

        // POST: api/Articles
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Article>> PostArticle(Article article)
        {
            _context.Articles.Add(article);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetArticle", new { id = article.IdArticle }, article);
        }

        // DELETE: api/Articles/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArticle(int? id)
        {
            var article = await _context.Articles.FindAsync(id);
            if (article == null)
            {
                return NotFound();
            }

            _context.Articles.Remove(article);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ArticleExists(int? id)
        {
            return _context.Articles.Any(e => e.IdArticle == id);
        }
    }
}
