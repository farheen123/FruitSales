namespace FruitSales.Domain.Pricing;

public interface IPricingStrategy
{
    string Description { get; }

    decimal CalculatePrice(decimal basePrice, decimal quantity);
}
