using BL.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class AuthController : ControllerBase
{

    private readonly ILogger<AuthController> _logger;
    private readonly AuthenticationService _authenticationService;

    public AuthController(ILogger<AuthController> logger,
                                    AuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService = authenticationService;
    }


    [HttpPost("register")]
    [AllowAnonymous]
    public void Register(string login, string firstName, string lastName, string password, string role)
    {

        _authenticationService.RegisterUser(login, firstName, lastName, password, role);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public ActionResult Login(string password, string login)
    {

        var token = _authenticationService.Login(login, password);
        return Ok(new { Token = token });
    }

}
