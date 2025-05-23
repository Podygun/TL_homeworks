using Application.Services.Utilities;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services.ReservationsServices;

public class ReservationsService : IReservationsService
{
    private readonly IReservationsRepository _reservationsRepository;
    private readonly IRoomTypesRepository _roomTypesRepository;

    public ReservationsService(
        IReservationsRepository reservationRepository,
        IRoomTypesRepository roomTypeRepository )
    {
        _reservationsRepository = reservationRepository;
        _roomTypesRepository = roomTypeRepository;
    }

    public async Task<Reservation> GetReservationByIdAsync( int id )
    {
        return await _reservationsRepository.GetByIdAsync( id );
    }

    public async Task<IEnumerable<Reservation>> GetReservationsByPropertyAsync( int propertyId )
    {
        return await _reservationsRepository.GetByPropertyIdAsync( propertyId );
    }

    public async Task<OperationResult<Reservation>> CreateReservationAsync( Reservation reservation )
    {
        if ( !await _reservationsRepository.IsRoomAvailableAsync(
            reservation.RoomTypeId,
            reservation.ArrivalDate,
            reservation.DepartureDate ) )
        {
            return OperationResult<Reservation>.Failed( "Номер недоступен на выбранные даты" );
        }

        var roomType = await _roomTypeRepository.GetByIdAsync( reservation.RoomTypeId );
        if ( roomType == null )
        {
            return OperationResult<Reservation>.Failed( "Тип номера не найден" );
        }

        reservation.CalculateTotal( roomType.DailyPrice );
        reservation.Currency = roomType.Currency;

        await _reservationRepository.AddAsync( reservation );
        return OperationResult<Reservation>.Success( reservation );
    }
}
