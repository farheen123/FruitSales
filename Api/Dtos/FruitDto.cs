namespace FruitSales.Api.Dtos;

/// <summary>
/// API-facing representation of a fruit available in the shop's catalog.
/// Kept separate from the domain Fruit class so the wire format can evolve
/// independently of the domain model.
/// </summary>
public sealed record FruitDto(string Name, decimal BasePrice, string Unit, string PricingDescription);