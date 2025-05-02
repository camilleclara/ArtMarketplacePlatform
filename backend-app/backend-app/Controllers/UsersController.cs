using BL.Models;
using BL.Models.Enums;
using BL.Services;
using BL.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{

    private readonly ILogger<UsersController> _logger;
    private readonly IUserService _userService;

    public UsersController(ILogger<UsersController> logger, IUserService userService)
    {
        _logger = logger;
        _userService = userService;
    }

    [Authorize(Roles = nameof(Roles.ADMIN))]
    [HttpGet("")]
    [ProducesResponseType(typeof(IEnumerable<UserSafeDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllUsers()
    {
        try
        {
            IEnumerable<UserDTO> lstReturned = await _userService.GetAllAsync();
            if (lstReturned.Count() == 0)
            {
                return NoContent();
            }
            return Ok(lstReturned);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "ARTISAN, CUSTOMER, ADMIN")]
    [HttpGet("{userId}")]
    [ProducesResponseType(typeof(IEnumerable<UserSafeDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetUserById(int userId)
    {
        try
        {
            UserSafeDTO user = await _userService.GetByIdAsync(userId);
            if (user == null)
            {
                return NoContent();
            }
            return Ok(user);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [Authorize(Roles = nameof(Roles.CUSTOMER))]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<UserSafeDTO>> UpdateUser(int id, [FromBody] UserSafeDTO userDto)
    {
        var updatedUser = await _userService.UpdateAsync(id, userDto);

        if (updatedUser == null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(updatedUser);
    }

}
