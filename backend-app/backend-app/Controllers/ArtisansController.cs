using BL.Models;
using BL.Models.Enums;
using BL.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
//[Authorize]
public class ArtisansController : ControllerBase
{

    private readonly ILogger<ArtisansController> _logger;
    private readonly IProductService _productService;

    public ArtisansController(ILogger<ArtisansController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [Authorize(Roles = nameof(Roles.ARTISAN))]
    [HttpGet("{artisanId}/products")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetProductsForArtisan(int artisanId)
    {
        try
        {
            IEnumerable<ProductDTO> lstReturned = await _productService.GetByArtisanId(artisanId);
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

    [Authorize(Roles = nameof(Roles.ARTISAN))]
    [HttpPost("{artisanId}/products")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [Produces("application/json")]
    public async Task<IActionResult> CreateProduct(int artisanId, [FromBody] ProductDTO product)
    {
        try
        {
            await _productService.AddAsyncForArtisan(product, artisanId);
            return StatusCode(StatusCodes.Status201Created);

        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }

    }
    [Authorize(Roles = nameof(Roles.ARTISAN))]
    [HttpPut("{artisanId}/products/{productId}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductDTO>> UpdateProductForArtisan(int artisanId, int productId, [FromBody] ProductDTO updateDto)
    {
        var updatedProduct = await _productService.UpdateAsync(productId, updateDto);

        if (updatedProduct == null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(updatedProduct);  // Retourne l'entité mise à jour
    }
    [Authorize(Roles = nameof(Roles.ARTISAN))]
    [HttpGet("{artisanId}/products/{productId}")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetProductById(int productId)
    {
        try
        {
            ProductDTO product = await _productService.GetByIdAsync(productId);
            if (product == null)
            {
                return NoContent();
            }
            return Ok(product);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }


    [Authorize(Roles = nameof(Roles.ARTISAN))]
    [HttpDelete("{artisanId}/products/{productId}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductDTO>> DeleteProductForArtisan(int artisanId, int productId)
    {
        //TODO check targeted product is artisan's
        var updatedProduct = await _productService.DeleteAsync(productId);
        return Ok(updatedProduct);  // Retourne l'entité mise à jour
    }
}
