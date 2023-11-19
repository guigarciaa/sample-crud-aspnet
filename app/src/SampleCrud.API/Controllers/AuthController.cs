using Microsoft.AspNetCore.Mvc;
using SampleCrud.Domain.Entities;
using SampleCrud.Domain.Services;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;
    private readonly ILogger<AuthController> _logger;

    public AuthController(ILogger<AuthController> logger, ITokenService tokenService, IUserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
        _logger = logger;
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User userCredentials)
    {
        if(userCredentials == null)
            return BadRequest("Invalid client request");

        if(!ModelState.IsValid)
            return BadRequest("Invalid client request");

        var token = _tokenService.GenerateJwtToken(userCredentials); 

        return Ok(new { token });
    }

    [HttpPost("register")]  
    public IActionResult Register([FromBody] User userCredentials)  
    {  
        if(userCredentials == null)
            return BadRequest("Invalid client request");

        if(!ModelState.IsValid)
            return BadRequest("Invalid client request");

        _userService.Add(userCredentials);

        return Ok(); 
    }

    [HttpPut("update")]
    public IActionResult Update([FromBody] User userCredentials)
    {
        if(userCredentials == null)
            return BadRequest("Invalid client request");

        if(!ModelState.IsValid)
            return BadRequest("Invalid client request");

        _userService.Update(userCredentials);

        return Ok();
    }   
}
