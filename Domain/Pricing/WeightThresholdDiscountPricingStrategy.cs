namespace FruitSales.Domain.Pricing;

public sealed class WeightThresholdDiscountPricingStrategy : IPricingStrategy
{
    private readonly PerKilogramPricingStrategy _baseStrategy = new();
    private readonly decimal _thresholdKg;
    private readonly decimal _discountPercentage;

    public WeightThresholdDiscountPricingStrategy(decimal thresholdKg, decimal discountPercentage)
    {
        if (thresholdKg < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(thresholdKg), "Threshold cannot be negative.");
        }

        if (discountPercentage is < 0 or > 1)
        {
            throw new ArgumentOutOfRangeException(
                nameof(discountPercentage),
                "Discount percentage must be between 0 and 1 (inclusive).");
        }

        _thresholdKg = thresholdKg;
        _discountPercentage = discountPercentage;
    }

    public string Description =>
        $"Per Kilogram with {_discountPercentage:P0} discount above {_thresholdKg}kg";

    public decimal CalculatePrice(decimal basePrice, decimal quantity)
    {
        var price = _baseStrategy.CalculatePrice(basePrice, quantity);

        if (quantity > _thresholdKg)
        {
            price *= 1 - _discountPercentage;
        }

        return price;
    }
}
