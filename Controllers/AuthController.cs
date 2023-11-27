using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LosCasaAngular.Models;
using LosCasaAngular.Services;
using System;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly IAuthService _authService;

    public AuthController(IAuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register(UserRegisterDto userRegisterDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authService.RegisterUserAsync(userRegisterDto);

        if (result.Success)
        {
            return Ok(new { Message = "Registration successful", UserId = result.UserId });
        }

        return BadRequest(new { Message = result.Errors });
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(UserLoginDto userLoginDto)
    {
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var result = await _authService.LoginUserAsync(userLoginDto);

        if (result.Success)
        {
            var tokenString = _authService.GenerateJwtToken(result.User);
            return Ok(new { Token = tokenString, Message = "Login successful" });
        }

        return BadRequest(new { Message = "Login failed" });
    }

    [HttpPost("logout")]
    public IActionResult Logout()
    {
        // Assuming your logout logic doesn't require server-side handling 
        // since JWT tokens are stateless. But if you maintain a token blacklist or 
        // manage sessions, you would handle that here.

        // No content indicates a successful request with no body to return
        return NoContent();
    }
}