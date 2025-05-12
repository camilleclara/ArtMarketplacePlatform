using BL.Services.Authentication;
using MarketPlaceException;
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
    public void Register(string login, string firstName, string lastName, string password, string role, string address)
    {

        _authenticationService.RegisterUser(login, firstName, lastName, password, role, address);
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public ActionResult Login(string password, string login)
    {

        try
        {
            var token = _authenticationService.Login(login, password);
            return Ok(new { Token = token });
        }
        catch(InvalidLoginOrPasswordException e)
        {
            return StatusCode(StatusCodes.Status401Unauthorized, e.Message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

}
