using Application.Dtos;
using Application.Services.Utilities;

namespace Application.Services.PropertiesService;

public interface IPropertiesService
{
    Task<PropertyDto?> GetByIdAsync( Guid id );

    Task<List<PropertyDto>?> GetAllAsync();

    Task<OperationResult> AddAsync( PropertyDto propertyDto );

    Task<OperationResult> UpdateAsync( PropertyDto propertyDto, Guid id );

    Task<OperationResult> DeleteByIdAsync( Guid id );
}
