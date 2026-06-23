using FruitSales.Api.Dtos;
using FruitSales.Domain.Fruits;
using Microsoft.AspNetCore.Mvc;

namespace FruitSales.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public sealed class FruitsController : ControllerBase
{
    private readonly FruitCatalog _catalog;

    public FruitsController(FruitCatalog catalog)
    {
        _catalog = catalog;
    }

    [HttpGet]
    [ProducesResponseType(typeof(List<FruitDto>), StatusCodes.Status200OK)]
    public ActionResult<List<FruitDto>> GetAll()
    {
        var dtos = _catalog.GetAll()
            .Select(f => new FruitDto(f.Name, f.BasePrice, f.Unit.ToString(), f.PricingStrategy.Description))
            .OrderBy(f => f.Name)
            .ToList();

        return Ok(dtos);
    }
}
