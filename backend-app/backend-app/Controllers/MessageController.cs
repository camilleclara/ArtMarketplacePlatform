using BL.Models;
using BL.Models.Enums;
using BL.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Authentication.Controllers;

[ApiController]
[Route("[controller]")]
public class MessageController : ControllerBase
{

    private readonly ILogger<MessageController> _logger;
    private readonly IMessageService _messageService;

    public MessageController(ILogger<MessageController> logger, IMessageService messageService)
    {
        _logger = logger;
        _messageService = messageService;
    }
    [Authorize(Roles = nameof(Roles.ADMIN))]
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Message>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetMessages()
    {
        try
        {
            IEnumerable<MessageDTO> lstReturned = await _messageService.GetAllAsync();
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
    [Authorize(Roles = nameof(Roles.ADMIN))]
    [HttpGet("{messageId}")]
    [ProducesResponseType(typeof(IEnumerable<Message>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetMessageById(int messageId)
    {
        try
        {
            MessageDTO message = await _messageService.GetByIdAsync(messageId);
            if (message == null)
            {
                return NoContent();
            }
            return Ok(message);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [Authorize(Roles = nameof(Roles.ADMIN))]
    [HttpGet("product/{productId}")]
    [ProducesResponseType(typeof(IEnumerable<Message>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetMessageByProductId(int productId)
    {
        try
        {
            IEnumerable<MessageDTO> messages = await _messageService.GetByProductId(productId);
            if (messages.Count() == 0)
            {
                return NoContent();
            }
            return Ok(messages);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [Authorize(Roles = "ARTISAN, CUSTOMER, ADMIN")]
    [HttpGet("user/{userId}")]
    [ProducesResponseType(typeof(IEnumerable<Message>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetMessageByUserId(int userId)
    {
        try
        {
            IEnumerable<MessageDTO> messages = await _messageService.GetByUserId(userId);
            if (messages.Count() == 0)
            {
                return NoContent();
            }
            return Ok(messages);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [Authorize(Roles = "ARTISAN, CUSTOMER, ADMIN")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] MessageDTO message)
    {
        try
        {
            await _messageService.AddAsync(message);
            return StatusCode(StatusCodes.Status201Created);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }
    [Authorize(Roles = nameof(Roles.ADMIN))]
    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<MessageDTO>> UpdateMessage(int id, [FromBody] MessageDTO updateDto)
    {
        var updatedMessage = await _messageService.UpdateAsync(id, updateDto);

        if (updatedMessage == null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(updatedMessage);  // Retourne l'entité mise à jour
    }
}
