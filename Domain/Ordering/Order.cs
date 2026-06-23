namespace FruitSales.Domain.Ordering;
public sealed class Order
{
    private readonly List<OrderLine> _lines = new();

    public IReadOnlyList<OrderLine> Lines => _lines;

    public void AddLine(OrderLine line)
    {
        _lines.Add(line ?? throw new ArgumentNullException(nameof(line)));
    }

    public decimal CalculateTotal() => _lines.Sum(line => line.LineTotal);
}
