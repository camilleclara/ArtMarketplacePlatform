using BL.Models;
using BL.Models.Enums;
using BL.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
namespace Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DeliveryController : ControllerBase
{

    private readonly ILogger<DeliveryController> _logger;
    private readonly IDeliveryService _deliveryService;

    public DeliveryController(ILogger<DeliveryController> logger, IDeliveryService deliveryService)
    {
        _logger = logger;
        _deliveryService = deliveryService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Delivery>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetDeliveries()
    {
        try
        {
            IEnumerable<DeliveryDTO> lstReturned = await _deliveryService.GetAllAsync();
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

    [HttpGet("{deliveryId}")]
    [ProducesResponseType(typeof(IEnumerable<Delivery>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetDeliveryById(int deliveryId)
    {
        try
        {
            DeliveryDTO delivery = await _deliveryService.GetByIdAsync(deliveryId);
            if (delivery == null)
            {
                return NoContent();
            }
            return Ok(delivery);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Produces("application/json")]
    public async Task<IActionResult> Post([FromBody] DeliveryDTO delivery)
    {
        try
        {
            await _deliveryService.AddAsync(delivery);
            return StatusCode(StatusCodes.Status201Created);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DeliveryDTO>> UpdateDelivery(int id, [FromBody] DeliveryDTO updateDto)
    {
        var updatedDelivery = await _deliveryService.UpdateAsync(id, updateDto);

        if (updatedDelivery == null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(updatedDelivery);  // Retourne l'entité mise à jour
    }

    [HttpPut("status/{id}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<DeliveryDTO>> UpdateDeliveryStatus(int orderId, [FromBody] DeliveryDTO updateDto)
    {
        var updatedDelivery = await _deliveryService.UpdateStatusAsync(orderId, updateDto);

        if (updatedDelivery == null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(updatedDelivery);  // Retourne l'entité mise à jour
    }
}
