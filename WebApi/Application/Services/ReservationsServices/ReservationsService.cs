using Application.Services.Utilities;
using Domain.Entities;
using Domain.Repositories;
using Domain.Utilities;

namespace Application.Services.ReservationsServices;

public class ReservationsService : IReservationsService
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IRoomTypesRepository _roomTypesRepository;
    private readonly IPropertiesRepository _propertiesRepository;

    public ReservationsService(
        IReservationsRepository reservationRepository,
        IRoomTypesRepository roomTypeRepository,
        IPropertiesRepository propertiesRepository )
    {
        _reservationsRepository = reservationRepository;
        _roomTypesRepository = roomTypeRepository;
        _propertiesRepository = propertiesRepository;
    }


    public async Task<Reservation> GetByIdAsync( int id )
    {
        try
        {
            Reservation? reservation = await _reservationsRepository.GetByIdAsync( id );

            return reservation;
        }
        catch
        {
            return null;
        }
    }

    public async Task<OperationResult> AddAsync( Reservation reservation )
    {
        RoomType? selectedRoomType = await _roomTypesRepository.GetByIdAsync( reservation.RoomTypeId );
        Property? property = await _propertiesRepository.GetByIdAsync( reservation.PropertyId );

        if ( selectedRoomType == null || property == null )
        {
            return OperationResult.BadRequest;
        }

        int availableRooms = await _roomTypesRepository.GetAmountAvailableRoomsAsync(
            reservation.RoomTypeId,
            reservation.PropertyId,
            reservation.ArrivalDateTime,
            reservation.DepartureDateTime );

        if ( availableRooms <= 0 )
        {
            return OperationResult.BadRequest;
        }


        reservation.Currency = selectedRoomType.Currency;
        reservation.Total = reservation.NightsCount * selectedRoomType.DailyPrice;

        // setup time for arrive and departure (12:00, 10:00)
        reservation.ArrivalDateTime = reservation.ArrivalDateTime.AddHours( 12 );
        reservation.DepartureDateTime = reservation.DepartureDateTime.AddHours( 10 );

        try
        {
            await _reservationsRepository.AddAsync( reservation );

            return OperationResult.Success;
        }
        catch
        {
            return OperationResult.ServerError;
        }

    }

    public async Task<OperationResult> DeleteByIdAsync( int id )
    {
        try
        {
            await _reservationsRepository.DeleteByIdAsync( id );

            return OperationResult.Success;
        }
        catch
        {
            return OperationResult.ServerError;
        }
    }

    public async Task<List<Reservation>?> GetFilteredReservationAsync( ReservationFilter filter )
    {
        try
        {
            IEnumerable<Reservation> reservations = await _reservationsRepository.GetFilteredReservationAsync( filter );

            return reservations.ToList();
        }
        catch
        {
            return null;
        }

    }

}
