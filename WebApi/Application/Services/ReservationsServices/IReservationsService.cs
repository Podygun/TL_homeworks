using Application.Services.Utilities;
using Domain.Entities;

namespace Application.Services.ReservationsServices;

public interface IReservationsService
{
    Task<Reservation> GetReservationByIdAsync( int id );
    Task<IEnumerable<Reservation>> GetReservationsByPropertyAsync( int propertyId );
    Task<OperationResult> CreateReservationAsync( Reservation reservation );
}
