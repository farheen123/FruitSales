using FruitSales.Api.Controllers;
using FruitSales.Api.Dtos;
using FruitSales.Domain.Fruits;
using FruitSales.Domain.Ordering;
using FruitSales.Domain.Pricing;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace FruitSales.Tests.UnitTests;

public class OrdersControllerTests
{
    private readonly OrdersController _controller;
    private readonly OrderCalculator _orderCalculator;
    private readonly FruitCatalog _fruitCatalog;

    public OrdersControllerTests()
    {
        _fruitCatalog = new FruitCatalog();
        _orderCalculator = new OrderCalculator(_fruitCatalog);
        _controller = new OrdersController(_orderCalculator);
    }

    [Fact]
    public void Calculate_WithValidRequest_ReturnsOkResult()
    {
        // Arrange
        var request = new OrderRequestDto(
            new List<OrderItemRequestDto>
            {
                new("Apple", 5m)
            });

        // Act
        var result = _controller.Calculate(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        Assert.Equal(200, okResult.StatusCode);
        var response = Assert.IsType<OrderResponseDto>(okResult.Value);
        Assert.NotNull(response);
    }

    [Fact]
    public void Calculate_WithValidMultipleItems_ReturnsCorrectTotal()
    {
        // Arrange
        var request = new OrderRequestDto(
     new List<OrderItemRequestDto>
     {
        new("Apple", 5m),
        new("Banana", 2m)
     });

        // Act
        var result = _controller.Calculate(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<OrderResponseDto>(okResult.Value);
        Assert.Equal(2, response.Lines.Count);
        Assert.NotEqual(0m, response.Total);
    }

    [Fact]
    public void Calculate_WithNullItems_ReturnsBadRequest()
    {
        // Arrange
        var request = new OrderRequestDto(null!);

        // Act
        var result = _controller.Calculate(request);

        // Assert
        var badResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badResult.StatusCode);
        Assert.Equal("An order must contain at least one item.", badResult.Value);
    }

    [Fact]
    public void Calculate_WithEmptyItems_ReturnsBadRequest()
    {
        // Arrange
        var request = new OrderRequestDto(new List<OrderItemRequestDto>());

        // Act
        var result = _controller.Calculate(request);

        // Assert
        var badResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badResult.StatusCode);
        Assert.Equal("An order must contain at least one item.", badResult.Value);
    }

    [Fact]
    public void Calculate_WithInvalidFruitName_ReturnsBadRequest()
    {
        // Arrange
        var request = new OrderRequestDto(
            new List<OrderItemRequestDto>
            {
                new("InvalidFruit", 5m)
            });

        // Act
        var result = _controller.Calculate(request);

        // Assert
        var badResult = Assert.IsType<BadRequestObjectResult>(result.Result);
        Assert.Equal(400, badResult.StatusCode);
    }

    [Fact]
    public void Calculate_WithValidRequest_ResponseIncludesCorrectLineInformation()
    {
        // Arrange
        var request = new OrderRequestDto(
            new List<OrderItemRequestDto>
            {
                new("Apple", 5m)
            });

        // Act
        var result = _controller.Calculate(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<OrderResponseDto>(okResult.Value);
        var line = Assert.Single(response.Lines);
        Assert.Equal("Apple", line.FruitName);
        Assert.Equal(5m, line.Quantity);
        Assert.NotNull(line.Unit);
        Assert.NotEqual(0m, line.BasePrice);
        Assert.NotEqual(0m, line.LineTotal);
    }

    [Fact]
    public void Calculate_WithValidRequest_TotalEqualsLineItems()
    {
        // Arrange
        var request = new OrderRequestDto(
            new List<OrderItemRequestDto>
            {
                new("Apple", 5m),
                new("Banana", 2m)
            });

        // Act
        var result = _controller.Calculate(request);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result.Result);
        var response = Assert.IsType<OrderResponseDto>(okResult.Value);
        var expectedTotal = response.Lines.Sum(line => line.LineTotal);
        Assert.Equal(expectedTotal, response.Total);
    }
}