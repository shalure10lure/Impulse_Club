using ImpulseClub.Entities;
using ImpulseClub.Entities.DTOS;
using ImpulseClub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ImpulseClub.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class ResourceController : ControllerBase
    {
        private readonly IResourceService _service;

        public ResourceController(IResourceService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var resources = await _service.GetAll();
            return Ok(resources);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetOne(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        [HttpPost]
        [Authorize(Policy = "FounderOrAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateResourceDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var created = await _service.CreateResource(dto, userId);
            
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "FounderOrAdmin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateResourceDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var updated = await _service.UpdateResource(dto, id, userId);
            
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "FounderOrAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _service.DeleteResource(id, userId);
            return NoContent();
        }

        // Reserve resource
        [HttpPost("{id:guid}/reserve")]
        [Authorize]
        public async Task<IActionResult> Reserve(Guid id, [FromBody] ResourceReservationDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _service.ReserveResource(id, userId, dto.Quantity);
            
            return Ok(new { message = "Resource successfully reserved" });
        }
    }
}