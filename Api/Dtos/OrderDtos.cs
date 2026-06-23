namespace FruitSales.Api.Dtos;

/// <summary>A single requested fruit + quantity, as sent by the client.</summary>
public sealed record OrderItemRequestDto(string FruitName, decimal Quantity);

/// <summary>The full order request payload: a list of items to price.</summary>
public sealed record OrderRequestDto(List<OrderItemRequestDto> Items);

/// <summary>A priced line in the response, including the calculated subtotal.</summary>
public sealed record OrderLineResponseDto(
    string FruitName,
    decimal Quantity,
    string Unit,
    decimal BasePrice,
    decimal LineTotal);

/// <summary>The full priced order response: every line plus the grand total.</summary>
public sealed record OrderResponseDto(List<OrderLineResponseDto> Lines, decimal Total);