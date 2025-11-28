using ImpulseClub.Entities;
using ImpulseClub.Entities.DTOS;
using ImpulseClub.Models.DTOS.ImpulseClub.Models.DTOS;
using ImpulseClub.Repositories;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace ImpulseClub.Services
{
    public class AuthService : IAuthService
    {
        private readonly IUserRepository _users;
        private readonly IConfiguration _configuration;

        public AuthService(IUserRepository users, IConfiguration configuration)
        {
            _users = users;
            _configuration = configuration;
        }

        public async Task<(bool ok, LoginResponseDto? response)> LoginAsync(LoginDto dto)
        {
            var user = await _users.GetByEmailAddress(dto.Email);
            if (user == null) return (false, null);

            var ok = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!ok) return (false, null);

            // Generate access/refresh token pair
            var (accessToken, expiresIn, jti) = GenerateJwtToken(user);
            var refreshToken = GenerateSecureRefreshToken();

            var refreshDays = int.Parse(_configuration["Jwt:RefreshDays"] ?? "14");

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(refreshDays);
            user.RefreshTokenRevokedAt = null;
            user.CurrentJwtId = jti;
            await _users.UpdateAsync(user);

            var resp = new LoginResponseDto
            {
                User = new UserDto { Id = user.Id, Username = user.Name, Email = user.Email },
                Role = user.Role,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresIn = expiresIn,
                TokenType = "Bearer"
            };

            return (true, resp);
        }

        public async Task<string> RegisterAsync(RegisterDto dto)
        {
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            var user = new User
            {
                Email = dto.Email,
                PasswordHash = hashedPassword,
                Name = dto.Username,
                Role = dto.Role
            };
            await _users.AddAsync(user);
            return user.Id.ToString();
        }

        public async Task<(bool ok, LoginResponseDto? response)> RefreshAsync(RefreshRequestDto dto)
        {
            // Find user with that refresh token
            var user = await _users.GetByRefreshToken(dto.RefreshToken);
            if (user == null) return (false, null);

            // Refresh validations
            if (user.RefreshToken != dto.RefreshToken) return (false, null);
            if (user.RefreshTokenRevokedAt.HasValue) return (false, null);
            if (!user.RefreshTokenExpiresAt.HasValue || user.RefreshTokenExpiresAt.Value < DateTime.UtcNow) return (false, null);

            // Rotation: generate new access + refresh and revoke the old one
            var (accessToken, expiresIn, jti) = GenerateJwtToken(user);
            var newRefresh = GenerateSecureRefreshToken();
            var refreshDays = int.Parse(_configuration["Jwt:RefreshDays"] ?? "14");

            user.RefreshToken = newRefresh;
            user.RefreshTokenExpiresAt = DateTime.UtcNow.AddDays(refreshDays);
            user.RefreshTokenRevokedAt = null;
            user.CurrentJwtId = jti;
            await _users.UpdateAsync(user);

            var resp = new LoginResponseDto
            {
                User = new UserDto { Id = user.Id, Username = user.Name, Email = user.Email },
                Role = user.Role,
                AccessToken = accessToken,
                RefreshToken = newRefresh,
                ExpiresIn = expiresIn,
                TokenType = "Bearer"
            };

            return (true, resp);
        }

        private (string token, int expiresInSeconds, string jti) GenerateJwtToken(User user)
        {
            var jwtSection = _configuration.GetSection("Jwt");
            var key = jwtSection["Key"]!;
            var issuer = jwtSection["Issuer"];
            var audience = jwtSection["Audience"];
            var expireMinutes = int.Parse(jwtSection["ExpiresMinutes"] ?? "60");

            var jti = Guid.NewGuid().ToString();

            var claims = new List<Claim> {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(JwtRegisteredClaimNames.Jti, jti),
            };

            var keyBytes = Encoding.UTF8.GetBytes(key);
            var creds = new SigningCredentials(new SymmetricSecurityKey(keyBytes), SecurityAlgorithms.HmacSha256);

            var expires = DateTime.UtcNow.AddMinutes(expireMinutes);

            var token = new JwtSecurityToken(
                issuer: issuer,
                audience: audience,
                claims: claims,
                expires: expires,
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);
            return (jwt, (int)TimeSpan.FromMinutes(expireMinutes).TotalSeconds, jti);
        }

        private static string GenerateSecureRefreshToken()
        {
            // 64 random bytes in Base64Url
            var bytes = RandomNumberGenerator.GetBytes(64);
            return Base64UrlEncoder.Encode(bytes);
        }
    }
}
