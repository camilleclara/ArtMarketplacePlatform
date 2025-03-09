using backend_app.Models;
using backend_app.Models.DTO;
using backend_app.Models.Enums;
using backend_app.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;

namespace Authentication.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
   
    private readonly ILogger<ProductController> _logger;
    private readonly IProductService _productService;

    public ProductController(ILogger<ProductController> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [Authorize(Roles = nameof(Roles.CUSTOMER))]
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

    [Authorize(Roles = nameof(Roles.CUSTOMER))]
    [HttpGet("{productId}")]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetProductById(int productId)
    {
        try
        {
            ProductDTO product = await _productService.GetById(productId);
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
    [Authorize(Roles = nameof(Roles.CUSTOMER))]
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
                IEnumerable<ProductDTO> products = await _productService.GetByCategory(category);
                if (products.Count() == 0)
                {
                    return NoContent();
                }
                return Ok(products);
            }

            throw new ArgumentException("Cat�gorie invalide");
            
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
    public async Task<ActionResult<ProductDTO>> UpdateGroup(int id, [FromBody] ProductDTO updateDto)
    {
        var updatedProduct = await _productService.UpdateAsync(id, updateDto);

        if (updatedProduct == null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(updatedProduct);  // Retourne l'entit� mise � jour
    }



    //[Authorize(Roles = nameof(Roles.ADMIN))]
    //[HttpGet("Admin")]
    //public ActionResult<IEnumerable<Product>> GetAdminProducts()
    //{
    //    IEnumerable<Product> products = new List<Product>
    //    {
    //        new Product { Id = 1, Name = "Produit Admin" },
    //        new Product { Id = 2, Name = "Produit Badmin" },
    //        new Product { Id = 3, Name = "Produit Cadmin" }
    //    };

    //    return Ok(products);
    //}





}
