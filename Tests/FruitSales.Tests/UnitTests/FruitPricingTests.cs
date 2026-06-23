using System;
using FruitSales.Domain.Fruits;
using FruitSales.Domain.Pricing;
using Xunit;

namespace FruitSales.Tests.UnitTests;

public class FruitPricingTests
{
    [Fact]
    public void PerItemPricing_WithIntegerQuantity_ReturnsCorrectTotal()
    {
        // Arrange
        var strategy = new PerItemPricingStrategy();
        var apple = new Fruit("Apple", 1.50m, PricingUnit.Item, strategy);

        // Act
        var total = apple.CalculatePrice(5m);

        // Assert
        Assert.Equal(7.50m, total);
    }

    [Fact]
    public void PerKilogramPricing_WithFractionalQuantity_ReturnsCorrectTotal()
    {
        // Arrange
        var strategy = new PerKilogramPricingStrategy();
        var banana = new Fruit("Banana", 2.30m, PricingUnit.Kilogram, strategy);

        // Act
        var total = banana.CalculatePrice(1.5m);

        // Assert
        Assert.Equal(3.45m, total);
    }

    [Fact]
    public void PerItemPricing_ThrowsOnNegativeQuantity()
    {
        // Arrange
        var strategy = new PerItemPricingStrategy();
        var apple = new Fruit("Apple", 1.50m, PricingUnit.Item, strategy);

        // Act & Assert
        Assert.Throws<ArgumentOutOfRangeException>(() => apple.CalculatePrice(-0.5m));
    }

    [Fact]
    public void FruitConstructor_ThrowsOnNullPricingStrategy()
    {
        // Act & Assert
        Assert.Throws<ArgumentNullException>(() => new Fruit("Orange", 1.00m, PricingUnit.Item, null!));
    }
}