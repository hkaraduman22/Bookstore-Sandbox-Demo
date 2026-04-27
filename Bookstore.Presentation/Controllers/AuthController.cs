using Microsoft.AspNetCore.Mvc;
using Bookstore.Services;

namespace Bookstore.Presentation.Controllers;

public record LoginDto(string Username, string Password);

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly AuthenticationService _authService;

    public AuthController(AuthenticationService authService)
    {
        _authService = authService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto dto)
    {
        if (dto == null || string.IsNullOrEmpty(dto.Username))
        {
            return BadRequest(new { message = "Geçersiz giriş bilgileri!" });
        }

        try
        {
            var token = _authService.CreateToken(dto.Username, dto.Password);

            string role;
            if (dto.Username.ToLower() == "admin")
            {
                role = "ADMIN";
            }
            else if (dto.Username.ToLower() == "seller")
            {
                role = "SELLER";
            }
            else
            {
                role = "BUYER";
            }

            return Ok(new { token, role });
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Giriş Hatası: {ex.Message}");
            return StatusCode(500, "Sunucu tarafında bir hata oluştu.");
        }
    }
}