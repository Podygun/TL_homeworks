using Application.Services.ReservationsServices;
using Application.Services.Utilities;
using Domain.Entities;
using Domain.Utilities;
using Microsoft.AspNetCore.Mvc;
using ReservationApi.Dtos.Reservations;
using ReservationApi.Mappers;

namespace ReservationApi.Controllers;

[ApiController]
[Route( "api/reservations" )]
public class ReservationController : ControllerBase
{
    private readonly IReservationsService _reservationsService;

    public ReservationController( IReservationsService reservationsService )
    {
        _reservationsService = reservationsService;
    }

    [HttpPost]
    public async Task<IActionResult> Create( [FromBody] CreateReservationDto dto )
    {
        if ( !ModelState.IsValid )
        {
            return BadRequest( ModelState );
        }

        OperationResult result = await _reservationsService.AddAsync( dto.ToDomain() );

        if ( result == OperationResult.Success )
        {
            return CreatedAtAction( nameof( Create ), dto );
        }

        return BadRequest( ModelState );
    }

    //список всех бронирований с поддержкой фильтрации
    [HttpGet]
    public async Task<IActionResult> GetFilteredReservations(
        [FromQuery] int? propertyId,
        [FromQuery] int? roomTypeId,
        [FromQuery] DateOnly? arrivalFrom,
        [FromQuery] DateOnly? arrivalTo,
        [FromQuery] DateOnly? departureFrom,
        [FromQuery] DateOnly? departureTo,
        [FromQuery] string? guestName,
        [FromQuery] string? guestPhone,
        [FromQuery] decimal? minTotal,
        [FromQuery] decimal? maxTotal
        )
    {
        ReservationFilter filter = new ReservationFilter
        {
            PropertyId = propertyId,
            RoomTypeId = roomTypeId,
            ArrivalDateFrom = arrivalFrom,
            ArrivalDateTo = arrivalTo,
            DepartureDateFrom = departureFrom,
            DepartureDateTo = departureTo,
            GuestName = guestName,
            GuestPhone = guestPhone,
            MinTotal = minTotal,
            MaxTotal = maxTotal
        };

        List<Reservation> reservations = await _reservationsService.GetFilteredReservationAsync( filter );

        if ( reservations == null )
        {
            return BadRequest();
        }

        return Ok( reservations.ToDto() );
    }


    [HttpGet( "{id:int}" )]
    public async Task<IActionResult> GetById( [FromRoute] int id )
    {
        Reservation? reservation = await _reservationsService.GetByIdAsync( id );

        if ( reservation == null )
        {
            return NotFound();
        }

        return Ok( reservation.ToDto() );
    }


    [HttpDelete( "{id:int}" )]
    public async Task<IActionResult> DeleteById( [FromRoute] int id )
    {
        OperationResult result = await _reservationsService.DeleteByIdAsync( id );

        if ( result == OperationResult.Success )
        {
            return Ok();
        }

        return BadRequest();
    }

}
