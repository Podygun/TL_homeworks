using Application.Mappers;
using Application.Services.PropertiesServices;
using Application.Services.Utilities;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using PropertiesApi.Dtos.Properties;

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
        List<Property>? properties = await _propertiesService.GetAllAsync();

        if ( properties == null )
        {
            return BadRequest();
        }

        List<PropertyDto> propertiesDto = properties.ToDto();

        return Ok( propertiesDto );
    }


    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] CreatePropertyDto createPropertyDto )
    {
        OperationResult result = await _propertiesService.AddAsync( createPropertyDto.ToDomain() );

        if ( result == OperationResult.Success )
        {
            return CreatedAtAction( nameof( Create ), createPropertyDto );
        }

        return BadRequest();
    }


    [HttpGet( "{propertyId:int}" )]
    public async Task<IActionResult> GetById( [FromRoute] int propertyId )
    {
        Property? property = await _propertiesService.GetByIdAsync( propertyId );

        if ( property == null )
        {
            return NotFound();
        }

        return Ok( property.ToDto() );

    }


    [HttpPut( "{id:int}" )]
    public async Task<IActionResult> Update( [FromBody] CreatePropertyDto propertyDto, [FromRoute] int id )
    {
        Property propertyToUpdate = propertyDto.ToDomain();

        propertyToUpdate.Id = id;

        OperationResult result = await _propertiesService.UpdateAsync( propertyToUpdate );

        if ( result == OperationResult.Success )
        {
            return Ok();
        }

        return BadRequest();
    }


    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> Delete( [FromRoute] int id )
    {
        OperationResult result = await _propertiesService.DeleteByIdAsync( id );

        if ( result == OperationResult.Success )
        {
            return Ok();
        }

        return BadRequest();
    }


}
