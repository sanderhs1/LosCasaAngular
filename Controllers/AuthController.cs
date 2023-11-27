using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using LosCasaAngular.Models;


namespace LosCasaAngular.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IUserService _userService;

    public AuthController(IUserService userService)
    {
        _userService = userService;
    }


    [HttpPost("login")]
    public async Task<ActionResult> Login([FromBody] UserLoginDto userLoginDto)

    {
        bool isValidUser = true; // This should be the result of your authentication check

        if (isValidUser)
        {

            var token = "synchronous_token_generation";
            return Ok(new { Token = token });
        }
        else
        {

            return Unauthorized();
        }
    }

    [HttpPost("register")]
    public async Task<ActionResult> Register([FromBody] UserLoginDto userLoginDto)
    {
        try
        {
            // Replace this with actual validation
            if (string.IsNullOrWhiteSpace(userLoginDto.Email) || string.IsNullOrWhiteSpace(userLoginDto.Password))
            {
                return BadRequest("Email and password are required.");
            }

            // Replace this with the actual logic to create a user
            var result = await _userService.Register(userLoginDto.Email, userLoginDto.Password);
            if (result)
            {
                return Ok(new { Message = "Registration successful" });
            }
            else
            {
                return BadRequest("Registration failed.");
            }
        }
        catch (Exception ex)
        {
            // Log the exception
            return StatusCode(500);
        }
    }
}
