using FruitSales.Domain.Fruits;

namespace FruitSales.Domain.Ordering;

public sealed class OrderCalculator
{
    private readonly FruitCatalog _catalog;

    public OrderCalculator(FruitCatalog catalog)
    {
        _catalog = catalog ?? throw new ArgumentNullException(nameof(catalog));
    }
    public Order BuildOrder(IEnumerable<OrderRequestItem> items)
    {
        if (items is null)
        {
            throw new ArgumentNullException(nameof(items));
        }

        var order = new Order();

        foreach (var item in items)
        {
            var fruit = _catalog.GetByName(item.FruitName);
            order.AddLine(new OrderLine(fruit, item.Quantity));
        }

        return order;
    }
}