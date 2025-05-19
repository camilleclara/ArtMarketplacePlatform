using BL.Models;
using BL.Models.Enums;
using BL.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace Authentication.Controllers;

[ApiController]
[Route("[controller]")]
public class ProductController : ControllerBase
{

    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetProducts()
    {
        try
        {
            IEnumerable<ProductDTO> lstReturned = await _productService.GetAllAsync();
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
    [HttpGet("admin")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetAllAdminProducts()
    {
        try
        {
            IEnumerable<ProductDTO> lstReturned = await _productService.GetAllAdminAsync();
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

    [HttpGet("{productId}")]
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

    [HttpGet("reviewable/{customerId}")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetReviewableProductIdsByCustomerId(int customerId)
    {
        try
        {
            List<int> productIds = await _productService.GetReviewableProductIdsByCustomerId(customerId);
            if (productIds == null)
            {
                return NoContent();
            }
            return Ok(productIds);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpGet("category/{categoryString}")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetProductByCategory(string categoryString)
    {
        try
        {
            if (Enum.TryParse<Category>(categoryString, out Category category))
            {
                IEnumerable<ProductDTO> products = await _productService.GetByCategoryAsync(category);
                if (products.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(products);
            }

            throw new ArgumentException("Catégorie invalide");

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
    public async Task<IActionResult> Post([FromBody] ProductDTO product)
    {
        try
        {
            await _productService.AddAsync(product);
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
    public async Task<ActionResult<ProductDTO>> UpdateProduct(int id, [FromBody] ProductDTO updateDto)
    {
        var updatedProduct = await _productService.UpdateAsync(id, updateDto);

        if (updatedProduct == null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(updatedProduct);  // Retourne l'entité mise à jour
    }
    [Authorize(Roles = nameof(Roles.ADMIN))]
    [HttpDelete("{productId}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductDTO>> DeleteProduct(int productId)
    {
        var updatedProduct = await _productService.PermanentDeleteAsync(productId);
        return Ok(updatedProduct);  // Retourne l'entité mise à jour
    }

    [Authorize(Roles = nameof(Roles.ADMIN))]
    [HttpPut("deactivate/{productId}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductDTO>> DeactivateProduct(int productId)
    {
        var updatedProduct = await _productService.DeleteAsync(productId);
        return Ok(updatedProduct);
    }

    [Authorize(Roles = nameof(Roles.ADMIN))]
    [HttpPut("approve/{productId}")]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public async Task<ActionResult<ProductDTO>> ApproveProduct(int productId)
    {
        var updatedProduct = await _productService.ApproveAsync(productId);
        return Ok(updatedProduct);
    }
}
