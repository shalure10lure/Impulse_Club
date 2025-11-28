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
    public class ClubController : ControllerBase
    {
        private readonly IClubService _service;

        public ClubController(IClubService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var clubs = await _service.GetAll();
            return Ok(clubs);
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
        public async Task<IActionResult> Create([FromBody] CreateClubDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var created = await _service.CreateClub(dto, userId);
            
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "FounderOrAdmin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateClubDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var updated = await _service.UpdateClub(dto, id, userId);
            
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> Delete(Guid id)
        {
            await _service.DeleteClub(id);
            return NoContent();
        }

        // User joins a club
        [HttpPost("{id:guid}/join")]
        [Authorize]
        public async Task<IActionResult> JoinClub(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _service.JoinClub(id, userId);
            
            return Ok(new { message = "You have successfully joined the club" });
        }

        // Get club members
        [HttpGet("{id:guid}/members")]
        [Authorize]
        public async Task<IActionResult> GetMembers(Guid id)
        {
            var members = await _service.GetClubMembers(id);
            return Ok(members);
        }
    }
}