using ApiIntro.DAL;
using ApiIntro.Dtos.Tag;
using ApiIntro.Entities;
using ApiIntro.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiIntro.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TagsController : ControllerBase
    {
        
        private readonly ITagRepository _repository;
        private readonly ITagService _service;

        public TagsController(ITagRepository repository,ITagService service)
        {
            
            _repository = repository;
            _service = service;
        }
        [HttpGet]
        public async Task<IActionResult> Get(int page, int take)
        {            
            return Ok(await _service.GetAllAsync(page,take));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            if (id <= 0) return BadRequest();   
            return Ok(await _service.GetAsync(id));
        }
        [HttpPost]
        public async Task<IActionResult> Create([FromForm]CreateTagDto createTagDto)
        {
            await _service.CreateAsync(createTagDto);
            return StatusCode(StatusCodes.Status201Created);
        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] UpdateTagDto updateTagDto )
        {
            if (id <= 0) return BadRequest();
            await _service.UpdateAsync(id, updateTagDto);
            return NoContent();

        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            if (id <= 0) return BadRequest();
            await _service.DeleteAsync(id);
            return NoContent();
        }


    }
}
