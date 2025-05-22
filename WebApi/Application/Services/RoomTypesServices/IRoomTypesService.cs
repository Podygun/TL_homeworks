using Application.Services.Utilities;
using Domain.Entities;

namespace Application.Services.RoomTypesServices;

public interface IRoomTypesService
{
    Task<RoomType?> GetByIdAsync( int id );

    Task<List<RoomType>?> GetAllAsync();

    Task<List<RoomType>?> GetByPropertyIdAsync( int propertyId );

    Task<OperationResult> AddAsync( RoomType roomType );

    Task<OperationResult> UpdateAsync( RoomType roomType );

    Task<OperationResult> DeleteByIdAsync( int id );
}
