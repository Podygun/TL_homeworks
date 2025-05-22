using Application.Services.Utilities;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services.RoomAmentitiesServices;

public sealed class RoomAmentitiesService : IRoomAmentitiesService
{
    private readonly IRoomAmentitiesRepository _amentitiesRepository;

    public RoomAmentitiesService( IRoomAmentitiesRepository amentitiesRepository )
    {
        _amentitiesRepository = amentitiesRepository;
    }

    public async Task<OperationResult> AddAsync( RoomAmentity roomAmentity )
    {
        try
        {
            await _amentitiesRepository.AddAsync( roomAmentity );

            return OperationResult.Success;
        }
        catch
        {
            return OperationResult.ServerError;
        }
    }

    public async Task<OperationResult> DeleteByIdAsync( Guid id )
    {
        try
        {
            await _amentitiesRepository.DeleteByIdAsync( id );

            return OperationResult.Success;
        }
        catch
        {
            return OperationResult.ServerError;
        }
    }

    public async Task<List<RoomAmentity>?> GetAllAsync()
    {
        try
        {
            List<RoomAmentity> amentities = await _amentitiesRepository.GetAllAsync();

            return amentities;
        }
        catch
        {
            return null;
        }
    }

    public async Task<RoomAmentity?> GetByIdAsync( Guid id )
    {
        try
        {
            RoomAmentity? property = await _amentitiesRepository.GetByIdAsync( id );

            return property;
        }
        catch
        {
            return null;
        }
    }

    public async Task<OperationResult> UpdateAsync( RoomAmentity roomAmentity )
    {
        try
        {
            await _amentitiesRepository.UpdateAsync( roomAmentity );

            return OperationResult.Success;
        }
        catch
        {
            return OperationResult.ServerError;
        }
    }
}
