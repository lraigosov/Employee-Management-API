using EmployeeManagement.Api.Models;
using EmployeeManagement.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace EmployeeManagement.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly TokenService _tokenService;

    public AuthController(TokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        if (_tokenService.ValidateCredentials(request.Username, request.Password))
        {
            var token = _tokenService.GenerateToken(request.Username);
            return Ok(new LoginResponse
            {
                Token = token,
                Expiration = DateTime.UtcNow.AddMinutes(60)
            });
        }

        return Unauthorized(new { message = "Invalid credentials" });
    }
}
