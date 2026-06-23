using FruitSales.Api.Dtos;
using FruitSales.Domain.Fruits;
using FruitSales.Domain.Ordering;
using Microsoft.AspNetCore.Mvc;

namespace FruitSales.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class OrdersController : ControllerBase
{
    private readonly OrderCalculator _orderCalculator;

    public OrdersController(OrderCalculator orderCalculator)
    {
        _orderCalculator = orderCalculator;
    }

    [HttpPost("calculate")]
    [ProducesResponseType(typeof(OrderResponseDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public ActionResult<OrderResponseDto> Calculate([FromBody] OrderRequestDto request)
    {
        if (request.Items is null || request.Items.Count == 0)
        {
            return BadRequest("An order must contain at least one item.");
        }

        try
        {
            var requestItems = request.Items
                .Select(i => new OrderRequestItem(i.FruitName, i.Quantity))
                .ToList();

            var order = _orderCalculator.BuildOrder(requestItems);

            var lineDtos = order.Lines
                .Select(line => new OrderLineResponseDto(
                    line.Fruit.Name,
                    line.Quantity,
                    line.Fruit.Unit.ToString(),
                    line.Fruit.BasePrice,
                    line.LineTotal))
                .ToList();

            return Ok(new OrderResponseDto(lineDtos, order.CalculateTotal()));
        }
        catch (KeyNotFoundException ex)
        {
            return BadRequest(ex.Message);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }
}