using ImpulseClub.Models.DTOS;
using ImpulseClub.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ImpulseClub.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class UserController:ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService service)
        {
            _service = service;
        }
        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _service.GetAll();
            return Ok(users);
        }

        [HttpGet("{id:guid}")]
        public async Task<IActionResult> GetById(Guid id)
        {
            var user = await _service.GetOne(id);
            if (user == null) return NotFound();
            return Ok(user);
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateUserDto dto)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);

            var updated = await _service.UpdateUser(dto, id);
            return CreatedAtAction(nameof(GetById), new { id }, updated);
        }

        [Authorize(Policy = "AdminOnly")]
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (!ModelState.IsValid) return ValidationProblem(ModelState);
            await _service.DeleteUser(id);
            return NoContent();
        }

    }
}
