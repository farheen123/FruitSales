using FruitSales.Domain.Pricing;

namespace FruitSales.Domain.Fruits;

public sealed class Fruit
{
    public string Name { get; }
    public decimal BasePrice { get; }
    public PricingUnit Unit { get; }
    public IPricingStrategy PricingStrategy { get; }

    public Fruit(string name, decimal basePrice, PricingUnit unit, IPricingStrategy pricingStrategy)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException("Fruit name cannot be empty.", nameof(name));
        }

        if (basePrice < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(basePrice), "Base price cannot be negative.");
        }

        Name = name;
        BasePrice = basePrice;
        Unit = unit;
        PricingStrategy = pricingStrategy ?? throw new ArgumentNullException(nameof(pricingStrategy));
    }

    public decimal CalculatePrice(decimal quantity) =>
        PricingStrategy.CalculatePrice(BasePrice, quantity);
}