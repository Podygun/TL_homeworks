using Domain.Entities;
using Domain.Utilities;

namespace Domain.Repositories;

public interface IReservationsRepository
{
    Task<Reservation?> GetByIdAsync( int id );

    Task AddAsync( Reservation reservation );

    Task DeleteByIdAsync( int id );

    Task<IEnumerable<Reservation>> GetFilteredReservationAsync( ReservationFilter filter );
}
