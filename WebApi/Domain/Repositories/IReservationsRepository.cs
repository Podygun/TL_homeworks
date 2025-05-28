using Domain.Entities;

namespace Domain.Repositories;

public interface IReservationsRepository : IDomainRepository<Reservation>
{
    Task<IEnumerable<Reservation>> GetByPropertyIdAsync( int propertyId );
    Task<IEnumerable<Reservation>> GetByDatesAsync( DateTime from, DateTime to );
    Task<bool> IsRoomAvailableAsync( int roomTypeId, DateTime arrivalDate, DateTime departureDate );

}
