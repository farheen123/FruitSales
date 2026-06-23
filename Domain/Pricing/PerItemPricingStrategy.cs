namespace FruitSales.Domain.Pricing;

public sealed class PerItemPricingStrategy : IPricingStrategy
{
    public string Description => "Per Item";

    public decimal CalculatePrice(decimal basePrice, decimal quantity)
    {
        if (quantity < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Item count cannot be negative.");
        }

        if (quantity != Math.Floor(quantity))
        {
            throw new ArgumentException("Item count must be a whole number.", nameof(quantity));
        }

        return basePrice * quantity;
    }
}
