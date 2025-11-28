using ImpulseClub.Entities;
using ImpulseClub.Models.DTOS;
using ImpulseClub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ImpulseClub.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class TrainingController : ControllerBase
    {
        private readonly ITrainingService _service;

        public TrainingController(ITrainingService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var trainings = await _service.GetAll();
            return Ok(trainings);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var item = await _service.GetOne(id);
            if (item == null) return NotFound();
            return Ok(item);
        }

        // Get trainings by club
        [HttpGet("club/{clubId:guid}")]
        public async Task<IActionResult> GetByClubId(Guid clubId)
        {
            var trainings = await _service.GetTrainingsByClub(clubId);
            return Ok(trainings);
        }

        [HttpPost]
        [Authorize(Policy = "FounderOrAdmin")]
        public async Task<IActionResult> Create([FromBody] CreateTrainingDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var created = await _service.CreateTraining(dto, userId);
            
            return CreatedAtAction(nameof(GetById), new { id = created.Id }, created);
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "FounderOrAdmin")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateTrainingDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var updated = await _service.UpdateTraining(dto, id, userId);
            
            return Ok(updated);
        }

        [HttpDelete("{id:guid}")]
        [Authorize(Policy = "FounderOrAdmin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _service.DeleteTraining(id, userId);
            return NoContent();
        }

        // User joins a training
        [HttpPost("{id:guid}/join")]
        [Authorize]
        public async Task<IActionResult> JoinTraining(Guid id)
        {
            var userId = Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            await _service.JoinTraining(id, userId);
            
            return Ok(new { message = "You have successfully joined the training" });
        }
    }
}