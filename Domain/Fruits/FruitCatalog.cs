using FruitSales.Domain.Pricing;

namespace FruitSales.Domain.Fruits;

public sealed class FruitCatalog
{
    private readonly Dictionary<string, Fruit> _fruitsByName;

    public FruitCatalog()
    {
        var fruits = new[]
        {
            new Fruit("Apple", 2.00m, PricingUnit.Kilogram, new PerKilogramPricingStrategy()),
            new Fruit("Banana", 0.30m, PricingUnit.Item, new PerItemPricingStrategy()),
            new Fruit(
                "Cherry",
                5.00m,
                PricingUnit.Kilogram,
                new WeightThresholdDiscountPricingStrategy(thresholdKg: 2m, discountPercentage: 0.10m)),
        };

        _fruitsByName = fruits.ToDictionary(f => f.Name, StringComparer.OrdinalIgnoreCase);
    }

    public IReadOnlyCollection<Fruit> GetAll() => _fruitsByName.Values;

    public Fruit GetByName(string name)
    {
        if (_fruitsByName.TryGetValue(name, out var fruit))
        {
            return fruit;
        }

        throw new KeyNotFoundException($"No fruit named '{name}' was found in the catalog.");
    }

    public bool TryGetByName(string name, out Fruit? fruit) => _fruitsByName.TryGetValue(name, out fruit);
}