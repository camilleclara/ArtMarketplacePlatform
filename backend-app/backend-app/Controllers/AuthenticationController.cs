using Authentication.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class AuthenticationController : ControllerBase
{

    private readonly ILogger<AuthenticationController> _logger;
    private readonly AuthenticationService _authenticationService;

    public AuthenticationController(ILogger<AuthenticationController> logger, 
                                    AuthenticationService authenticationService)
    {
        _logger = logger;
        _authenticationService=authenticationService;
    }

   
   [HttpPost("Register")]
   [AllowAnonymous]
    public void Register(string password, string login){

        _authenticationService.RegisterUser(login, password);
    }

   [HttpPost("Login")]
   [AllowAnonymous]
    public ActionResult Login(string password, string login){

        var token= _authenticationService.Login(login, password);
          return Ok(new {Token = token});
    }

    
}
