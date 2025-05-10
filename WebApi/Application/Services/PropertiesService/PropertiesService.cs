using Application.Dtos;
using Application.Mappers;
using Application.Services.Utilities;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services.PropertiesService;

public sealed class PropertiesService : IPropertiesService
{
    private readonly IPropertiesRepository _propertiesRepository;
    private readonly IMapper<Property, PropertyDto> _propertiesMapper;

    public PropertiesService(
        IPropertiesRepository propertiesRepository,
        IMapper<Property, PropertyDto> propertiesMapper )
    {
        _propertiesRepository = propertiesRepository;
        _propertiesMapper = propertiesMapper;
    }


    public async Task<OperationResult> AddAsync( PropertyDto propertyDto )
    {
        Property property = _propertiesMapper.Convert( propertyDto );

        property.Id = Guid.NewGuid();

        OperationResult result = new();

        try
        {
            await _propertiesRepository.AddAsync( property );
        }
        catch
        {
            result.ErrorMessage = $"Создание не удалось. Пожалуйста, проверьте введенную информацию и попробуйте снова.";
            result.Success = false;
        }

        return result;
    }


    public async Task<OperationResult> DeleteByIdAsync( Guid id )
    {
        OperationResult result = new();

        try
        {
            await _propertiesRepository.DeleteByIdAsync( id );
        }
        catch
        {
            result.ErrorMessage = $"Удаление не удалось. Пожалуйста, повторите позже.";
            result.Success = false;
        }

        return result;
    }


    /// <summary>
    /// Retrieves all dto properties from the repository.
    /// </summary>
    /// <returns>
    /// List of properties, or null if an error occurs.
    /// </returns>
    public async Task<List<PropertyDto>?> GetAllAsync()
    {
        try
        {
            List<Property> properties = await _propertiesRepository.GetAllAsync();

            return _propertiesMapper.Convert( properties );
        }
        catch
        {
            return null;
        }
    }


    public async Task<PropertyDto?> GetByIdAsync( Guid id )
    {
        try
        {
            Property? property = await _propertiesRepository.GetByIdAsync( id );

            if ( property is null )
            {
                return null;
            }

            return _propertiesMapper.Convert( property );
        }
        catch
        {
            return null;
        }
    }


    public async Task<OperationResult> UpdateAsync( PropertyDto propertyDto, Guid propertyId )
    {
        Property property = _propertiesMapper.Convert( propertyDto );
        property.Id = propertyId;

        OperationResult result = new();

        try
        {
            await _propertiesRepository.UpdateAsync( property );
        }
        catch
        {
            result.ErrorMessage = $"Обновление не удалось. Пожалуйста, проверьте введенную информацию и попробуйте снова.";
            result.Success = false;
        }

        return result;
    }
}
