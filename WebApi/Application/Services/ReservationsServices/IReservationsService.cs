using Application.Services.Utilities;
using Domain.Entities;
using Domain.Utilities;

namespace Application.Services.ReservationsServices;

public interface IReservationsService
{
    Task<Reservation> GetByIdAsync( int id );
    Task<OperationResult> AddAsync( Reservation reservation );
    Task<OperationResult> DeleteByIdAsync( int id );
    Task<List<Reservation>?> GetFilteredReservationAsync( ReservationFilter filter);
}
