using ImpulseClub.Entities;
using ImpulseClub.Models.DTOS;
using ImpulseClub.Services;
using Microsoft.AspNetCore.Mvc;

namespace ImpulseClub.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ClubController : ControllerBase
    {
        private readonly IClubService _service;
        public ClubController(IClubService service) { _service = service; }

        [HttpGet]
        public async Task<IActionResult> GetAll() => Ok(await _service.GetAll());

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetOne(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateClubDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var created = await _service.CreateClub(dto);
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClubDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            var updated = await _service.UpdateClub(dto, id);
            return CreatedAtAction(nameof(GetById), new { id = updated.Id }, updated);
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteClub(id);
            return NoContent();
        }
    }
}