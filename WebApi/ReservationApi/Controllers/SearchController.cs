using Application.Services.PropertiesServices;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Mappers;

namespace ReservationApi.Controllers;

[ApiController]
[Route( "api/search" )]
public class SearchController : ControllerBase
{
    private readonly IPropertiesService _propertiesService;

    public SearchController( IPropertiesService propertySearchService )
    {
        _propertiesService = propertySearchService;
    }

    [HttpGet( "test_search" )]
    public async Task<IActionResult> TestSearch()
    {
        string city = "Barcelona";
        DateOnly arrivalDate = DateOnly.Parse( "2025-05-28" );
        DateOnly departureDate = DateOnly.Parse( "2025-05-29" );
        int guests = 2;
        decimal maxPrice = 500;

        List<Property>? foundProperties = await _propertiesService.SearchPropertiesAsync(
            city,
            arrivalDate.ToDateTime( TimeOnly.MinValue ),
            departureDate.ToDateTime( TimeOnly.MinValue ),
            guests,
            maxPrice );

        if ( foundProperties == null )
        {
            return BadRequest();
        }

        if ( foundProperties.Count == 0 )
        {
            return NotFound();
        }

        return Ok( foundProperties.ToDto() );
    }

    [HttpGet( "search" )]
    public async Task<IActionResult> Search(
        [FromQuery] string city,
        [FromQuery] DateOnly arrivalDate,
        [FromQuery] DateOnly departureDate,
        [FromQuery] int guests,
        [FromQuery] decimal? maxPrice )
    {
        List<Property>? foundProperties = await _propertiesService.SearchPropertiesAsync(
            city,
            arrivalDate.ToDateTime( TimeOnly.MinValue ),
            departureDate.ToDateTime( TimeOnly.MinValue ),
            guests,
            maxPrice );

        if ( foundProperties == null )
        {
            return BadRequest();
        }

        if ( foundProperties.Count == 0 )
        {
            return NotFound();
        }

        return Ok( foundProperties.ToDto() );
    }
}
