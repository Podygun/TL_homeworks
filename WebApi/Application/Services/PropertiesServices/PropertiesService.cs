using Application.Services.Utilities;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services.PropertiesServices;

public sealed class PropertiesService : IPropertiesService
{
    private readonly IPropertiesRepository _propertiesRepository;

    public PropertiesService( IPropertiesRepository propertiesRepository )
    {
        _propertiesRepository = propertiesRepository;
    }


    public async Task<OperationResult> AddAsync( Property property )
    {
        try
        {
            await _propertiesRepository.AddAsync( property );

            return OperationResult.Success;
        }
        catch
        {
            return OperationResult.ServerError;
        }
    }


    public async Task<OperationResult> DeleteByIdAsync( int id )
    {
        try
        {
            await _propertiesRepository.DeleteByIdAsync( id );

            return OperationResult.Success;
        }
        catch
        {
            return OperationResult.ServerError;
        }
    }

    /// <summary>
    /// Retrieves all Properties list in DB
    /// </summary>
    /// <returns>
    /// List of Property if success
    /// null if internal error
    /// </returns>
    public async Task<List<Property>?> GetAllAsync()
    {
        try
        {
            List<Property> properties = await _propertiesRepository.GetAllAsync();

            return properties;
        }
        catch
        {
            return null;
        }
    }


    public async Task<Property?> GetByIdAsync( int id )
    {
        try
        {
            Property? property = await _propertiesRepository.GetByIdAsync( id );

            return property;
        }
        catch
        {
            return null;
        }
    }

    public async Task<OperationResult> UpdateAsync( Property property )
    {
        try
        {
            await _propertiesRepository.UpdateAsync( property );

            return OperationResult.Success;
        }
        catch
        {
            return OperationResult.ServerError;
        }
    }
}
