using BL.Models;
using BL.Models.Enums;
using BL.Services.Interfaces;
using Domain;
using Microsoft.AspNetCore.Mvc;
namespace Authentication.Controllers;

[ApiController]
[Route("[controller]")]
public class ReviewController : ControllerBase
{

    private readonly ILogger<ReviewController> _logger;
    private readonly IReviewService _reviewService;

    public ReviewController(ILogger<ReviewController> logger, IReviewService reviewService)
    {
        _logger = logger;
        _reviewService = reviewService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Review>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetReviews()
    {
        try
        {
            IEnumerable<ReviewDTO> lstReturned = await _reviewService.GetAllAsync();
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

    [HttpGet("{reviewId}")]
    [ProducesResponseType(typeof(IEnumerable<Review>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetReviewById(int reviewId)
    {
        try
        {
            ReviewDTO review = await _reviewService.GetByIdAsync(reviewId);
            if (review == null)
            {
                return NoContent();
            }
            return Ok(review);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
    [HttpGet("product/{productId}")]
    [ProducesResponseType(typeof(IEnumerable<Review>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetReviewByProductId(int productId)
    {
        try
        {
            IEnumerable<ReviewDTO> reviews = await _reviewService.GetByProductId(productId);
            if (reviews.Count() == 0)
            {
                return NoContent();
            }
            return Ok(reviews);
        }
        catch (Exception ex)
        {
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("artisan/{artisanId}")]
    [ProducesResponseType(typeof(IEnumerable<Review>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    public async Task<IActionResult> GetReviewByArtisanId(int artisanId)
    {
        try
        {
            IEnumerable<ReviewDTO> reviews = await _reviewService.GetByArtisanId(artisanId);
            if (reviews.Count() == 0)
            {
                return NoContent();
            }
            return Ok(reviews);
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
    public async Task<IActionResult> Post([FromBody] ReviewDTO review)
    {
        try
        {
            await _reviewService.AddAsync(review);
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
    public async Task<ActionResult<ReviewDTO>> UpdateReview(int id, [FromBody] ReviewDTO updateDto)
    {
        var updatedReview = await _reviewService.UpdateAsync(id, updateDto);

        if (updatedReview == null)
            return StatusCode(StatusCodes.Status404NotFound);

        return Ok(updatedReview);  // Retourne l'entité mise à jour
    }
}
