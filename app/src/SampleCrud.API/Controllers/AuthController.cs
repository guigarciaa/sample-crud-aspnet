using Microsoft.AspNetCore.Mvc;
using SampleCrud.Domain.DTO;
using SampleCrud.Domain.Services;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger, ITokenService tokenService)
    {
        _tokenService = tokenService;
        _logger = logger;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] UserDTO userCredentials)
    {
        if (userCredentials == null)
            return BadRequest("Invalid client request");

        if (!ModelState.IsValid)
            return BadRequest("Invalid client request");

        var token = _tokenService.GenerateJwtToken(userCredentials);

        return Ok(new { token });
    }
}
