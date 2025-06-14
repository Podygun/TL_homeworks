using Application.Mappers;
using Application.Services.RoomTypesServices;
using Application.Services.Utilities;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using PropertiesApi.Dtos.RoomTypes;
using PropertiesApi.Mappers;

namespace PropertiesApi.Controllers;


[Controller]
[Route( "api/roomtypes" )]
public sealed class RoomTypesController : ControllerBase
{
    private readonly IRoomTypesService _roomTypesService;

    public RoomTypesController( IRoomTypesService roomTypesService )
    {
        _roomTypesService = roomTypesService;
    }


    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        List<RoomType>? roomTypes = await _roomTypesService.GetAllAsync();

        if ( roomTypes == null )
        {
            return NotFound();
        }

        List<RoomTypeDto> roomTypesDto = roomTypes.ToDto();

        return Ok( roomTypesDto );
    }


    [HttpGet( "{propertyId}/roomtypes" )]
    public async Task<IActionResult> GetRoomTypesByPropertyId( [FromRoute] int propertyId )
    {
        List<RoomType>? roomTypes = await _roomTypesService.GetByPropertyIdAsync( propertyId );

        if ( roomTypes == null )
        {
            return NotFound();
        }

        return Ok( roomTypes.ToDto() );
    }


    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] CreateRoomTypeDto createRoomTypeDto )
    {
        OperationResult result = await _roomTypesService.AddAsync( createRoomTypeDto.ToDomain() );

        if ( result == OperationResult.Success )
        {
            return CreatedAtAction( nameof( Create ), createRoomTypeDto );
        }

        return BadRequest();
    }


    [HttpGet( "{roomTypeId:int}" )]
    public async Task<IActionResult> GetById( [FromRoute] int roomTypeId )
    {
        RoomType? roomType = await _roomTypesService.GetByIdAsync( roomTypeId );

        if ( roomType == null )
        {
            return NotFound();
        }

        return Ok( roomType.ToDto() );
    }


    [HttpPut( "{id:int}" )]
    public async Task<IActionResult> Update( [FromBody] RoomTypeDto roomTypeDto, [FromRoute] int id )
    {
        roomTypeDto.Id = id;

        OperationResult result = await _roomTypesService.UpdateAsync( roomTypeDto.ToDomain() );

        if ( result == OperationResult.Success )
        {
            return Ok();
        }

        return BadRequest();
    }


    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> Delete( [FromRoute] int id )
    {
        OperationResult result = await _roomTypesService.DeleteByIdAsync( id );

        if ( result == OperationResult.Success )
        {
            return Ok();
        }

        return BadRequest();
    }

}
