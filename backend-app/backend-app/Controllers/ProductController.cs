using backend_app.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Authentication.Controllers;

[ApiController]
[Route("[controller]")]
[Authorize]
public class ProductController : ControllerBase
{
    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private readonly ILogger<ProductController> _logger;

    public ProductController(ILogger<ProductController> logger)
    {
        _logger = logger;
    }

    [Authorize(Roles = nameof(Roles.USER))]
    [HttpGet]
    public ActionResult<IEnumerable<Product>> GetProducts()
    {
        IEnumerable<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Produit A" },
            new Product { Id = 2, Name = "Produit B" },
            new Product { Id = 3, Name = "Produit C" }
        };

        return Ok(products);
    }

    [Authorize(Roles = nameof(Roles.ADMIN))]
    [HttpGet("Admin")]
    public ActionResult<IEnumerable<Product>> GetAdminProducts()
    {
        IEnumerable<Product> products = new List<Product>
        {
            new Product { Id = 1, Name = "Produit Admin" },
            new Product { Id = 2, Name = "Produit Badmin" },
            new Product { Id = 3, Name = "Produit Cadmin" }
        };

        return Ok(products);
    }



}
