using Application.Dtos;
using Application.Services.PropertiesService;
using Application.Services.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace PropertiesApi.Controllers;

[Controller]
[Route( "api/properties" )]
public sealed class PropertiesController : ControllerBase
{
    private readonly IPropertiesService _propertiesService;

    public PropertiesController( IPropertiesService propertiesService )
    {
        _propertiesService = propertiesService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<PropertyDto>? properties = await _propertiesService.GetAllAsync();

        return Ok( properties );
    }

    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] PropertyDto propertyDto )
    {
        OperationResult result = await _propertiesService.AddAsync( propertyDto );

        if ( result.Success )
        {
            return Ok( result );
        }

        return BadRequest();
    }

    [HttpGet( "{propertyId:guid}" )]
    public async Task<IActionResult> GetById( [FromRoute] Guid propertyId )
    {
        PropertyDto? property = await _propertiesService.GetByIdAsync( propertyId );

        if ( property is null )
        {
            return NotFound();
        }

        return Ok( property );

    }

    [HttpPut( "{id:guid}" )]
    public async Task<IActionResult> Update( [FromBody] PropertyDto propertyDto, [FromRoute] Guid id )
    {
        OperationResult result = await _propertiesService.UpdateAsync( propertyDto, id );

        if ( result.Success )
        {
            return Ok();
        }

        return BadRequest();
    }

    [HttpDelete( "{id:guid}" )]
    public async Task<IActionResult> Delete( [FromRoute] Guid id )
    {
        OperationResult result = await _propertiesService.DeleteByIdAsync( id );

        if ( result.Success )
        {
            return Ok();
        }

        return BadRequest();
    }
}
