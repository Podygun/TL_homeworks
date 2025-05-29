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


        reservation.Currency = selectedRoomType.Currency;
        reservation.Total = reservation.NightsCount * selectedRoomType.DailyPrice;

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



    //public async Task<Reservation> GetReservationByIdAsync( int id )
    //{
    //    return await _reservationsRepository.GetByIdAsync( id );
    //}

    //public async Task<IEnumerable<Reservation>> GetReservationsByPropertyAsync( int propertyId )
    //{
    //    return await _reservationsRepository.GetByPropertyIdAsync( propertyId );
    //}

    //public async Task<OperationResult<Reservation>> CreateReservationAsync( Reservation reservation )
    //{
    //    if ( !await _reservationsRepository.IsRoomAvailableAsync(
    //        reservation.RoomTypeId,
    //        reservation.ArrivalDate,
    //        reservation.DepartureDate ) )
    //    {
    //        return OperationResult<Reservation>.Failed( "Номер недоступен на выбранные даты" );
    //    }

    //    var roomType = await _roomTypeRepository.GetByIdAsync( reservation.RoomTypeId );
    //    if ( roomType == null )
    //    {
    //        return OperationResult<Reservation>.Failed( "Тип номера не найден" );
    //    }

    //    reservation.CalculateTotal( roomType.DailyPrice );
    //    reservation.Currency = roomType.Currency;

    //    await _reservationRepository.AddAsync( reservation );
    //    return OperationResult<Reservation>.Success( reservation );
    //}
}
