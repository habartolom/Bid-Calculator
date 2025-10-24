using MediatR;
using Microsoft.AspNetCore.Mvc;
using Progi.BidCalculator.Application.DTOs;
using Progi.BidCalculator.Application.Queries;

namespace Progi.BidCalculator.API.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class BidCalculatorController(
    IMediator mediator,
    ILogger<BidCalculatorController> logger
) : ControllerBase
{
    private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
    private readonly ILogger<BidCalculatorController> _logger = logger ?? throw new ArgumentNullException(nameof(logger));

    [HttpPost("calculate")]
    [ProducesResponseType(typeof(BidCalculationResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
    public async Task<ActionResult<BidCalculationResponse>> Calculate([FromBody] BidCalculationRequest request, CancellationToken cancellationToken)
    {
        _logger.LogInformation("Calculating bid for vehicle type {VehicleType} with base price {Price}", request.VehicleType, request.VehicleBasePrice );

        var query = new CalculateBidQuery(request.VehicleBasePrice, request.VehicleType);
        var response = await _mediator.Send(query, cancellationToken);

        _logger.LogInformation("Calculation completed. Total cost: {TotalCost}", response.TotalCost);

        return Ok(response);
    }

    [HttpGet("health")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public IActionResult Health()
    {
        return Ok(new { status = "Healthy", timestamp = DateTime.UtcNow });
    }
}

