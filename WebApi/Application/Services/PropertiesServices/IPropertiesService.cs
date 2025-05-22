using Application.Services.Utilities;
using Domain.Entities;

namespace Application.Services.PropertiesServices;

public interface IPropertiesService
{
    Task<Property?> GetByIdAsync( Guid id );

    Task<List<Property>?> GetAllAsync();

    Task<OperationResult> AddAsync( Property property );

    Task<OperationResult> UpdateAsync( Property property );

    Task<OperationResult> DeleteByIdAsync( Guid id );
}
