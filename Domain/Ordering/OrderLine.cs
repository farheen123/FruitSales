using FruitSales.Domain.Fruits;

namespace FruitSales.Domain.Ordering;
public sealed class OrderLine
{
    public Fruit Fruit { get; }
    public decimal Quantity { get; }

    public OrderLine(Fruit fruit, decimal quantity)
    {
        Fruit = fruit ?? throw new ArgumentNullException(nameof(fruit));

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        }

        Quantity = quantity;
    }

    public decimal LineTotal => Fruit.CalculatePrice(Quantity);
}
