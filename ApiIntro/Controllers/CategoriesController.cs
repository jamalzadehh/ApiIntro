using ApiIntro.DAL;
using ApiIntro.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiIntro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : ControllerBase
    {
        private readonly AppDbContext _context;

        public CategoriesController(AppDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page,int take)
        {
            List<Category> categories = await _context.Categories.Skip((page-1)*take).Take(take).ToListAsync();
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();
            Category category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create(Category category)
        {
            await _context.Categories.AddAsync(category);
            await _context.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= null) return BadRequest();
            Category existed = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();
            existed.Name = name;
            await _context.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Category existed = await _context.Categories.FirstOrDefaultAsync(x => x.Id == id);
            if (existed == null) return NotFound();
            _context.Categories.Remove(existed);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
