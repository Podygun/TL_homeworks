using Application.Services.Utilities;
using Domain.Entities;

namespace Application.Services.RoomAmentitiesServices;

public interface IRoomAmentitiesService
{
    Task<RoomAmentity?> GetByIdAsync( Guid id );

    Task<List<RoomAmentity>?> GetAllAsync();

    Task<OperationResult> AddAsync( RoomAmentity roomAmentity );

    Task<OperationResult> UpdateAsync( RoomAmentity roomAmentity );

    Task<OperationResult> DeleteByIdAsync( Guid id );
}
