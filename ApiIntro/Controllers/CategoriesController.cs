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
        private readonly IRepository _repository;

        public CategoriesController(AppDbContext context,IRepository repository)
        {
            _context = context;
            _repository = repository;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page,int take)
        {
            IEnumerable<Category> categories = await _repository.GetAllAsync(c=>c.Id>4);
            return Ok(categories);
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();
            Category category = await _repository.GetByIdAsync(id);
            if (category == null) return NotFound();
            return Ok(category);
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateCategoryDto categoryDto)
        {
            Category category = new Category 
            {
                Name = categoryDto.Name,
            };
            await _repository.AddAsync(category);
            await _repository.SaveChangesAsync();
            return StatusCode(StatusCodes.Status201Created, category);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, string name)
        {
            if (id <= null) return BadRequest();
            Category existed = await _repository.GetByIdAsync(id);
            if (existed == null) return NotFound();
            existed.Name = name;
            _repository.Update(existed);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            Category existed = await _repository.GetByIdAsync(id);
            if (existed == null) return NotFound();
            _repository.Delete(existed);
            await _repository.SaveChangesAsync();
            return NoContent();
        }
    }
}
