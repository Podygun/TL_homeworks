using Domain.Entities;

namespace Domain.Repositories;

public interface IRoomTypesRepository : IDomainRepository<RoomType>
{
    Task<List<RoomType>> GetByPropertyId( int propertyId );
}