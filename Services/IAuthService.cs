using ImpulseClub.Models.DTOS;
using ImpulseClub.Models.DTOS.ImpulseClub.Models.DTOS;

namespace ImpulseClub.Services
{
    public interface IAuthService
    {
        Task<(bool ok, LoginResponseDto? response)> LoginAsync(LoginDto dto);
        Task<string> RegisterAsync(RegisterDto dto);
        Task<(bool ok, LoginResponseDto? response)> RefreshAsync(RefreshRequestDto dto);
    }
}
