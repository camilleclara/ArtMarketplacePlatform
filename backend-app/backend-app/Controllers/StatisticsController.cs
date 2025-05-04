using BL.Models;
using BL.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using BL.Models.Enums;
using BL.Models.Statistics;

namespace Authentication.Controllers;

[ApiController]
[Route("api/[controller]")]
public class StatisticsController : ControllerBase
{
    private readonly ILogger<StatisticsController> _logger;
    private readonly IStatisticsService _statisticsService;

    public StatisticsController(ILogger<StatisticsController> logger, IStatisticsService statisticsService)
    {
        _logger = logger;
        _statisticsService = statisticsService;
    }

    [HttpGet("orders")]
    [ProducesResponseType(typeof(OrderStatisticsDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetOrderStatistics()
    {
        try
        {
            var statistics = await _statisticsService.GetOrderStatisticsAsync();
            return Ok(statistics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving order statistics");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("trending-products")]
    [ProducesResponseType(typeof(IEnumerable<TrendingProductDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetTrendingProducts([FromQuery] int limit = 10)
    {
        try
        {
            var trendingProducts = await _statisticsService.GetTrendingProductsAsync(limit);
            return Ok(trendingProducts);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving trending products");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("user-activity")]
    [ProducesResponseType(typeof(UserActivityStatisticsDTO), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetUserActivityStatistics()
    {
        try
        {
            var statistics = await _statisticsService.GetUserActivityStatisticsAsync();
            return Ok(statistics);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving user activity statistics");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }

    [HttpGet("recent-orders")]
    [ProducesResponseType(typeof(IEnumerable<OrderDTO>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetRecentOrders([FromQuery] int limit = 10)
    {
        try
        {
            var recentOrders = await _statisticsService.GetRecentOrdersAsync(limit);
            return Ok(recentOrders);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error retrieving recent orders");
            return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
        }
    }
}