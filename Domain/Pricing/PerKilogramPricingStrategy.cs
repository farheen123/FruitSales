namespace FruitSales.Domain.Pricing;

public sealed class PerKilogramPricingStrategy : IPricingStrategy
{
    public string Description => "Per Kilogram";

    public decimal CalculatePrice(decimal basePrice, decimal quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Weight cannot be negative.");
        }

        return basePrice * quantity;
    }
}
