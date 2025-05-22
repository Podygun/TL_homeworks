using Application.Services.Utilities;
using Domain.Entities;
using Domain.Repositories;

namespace Application.Services.RoomTypesServices;

public sealed class RoomTypesService : IRoomTypesService
{
    private readonly IRoomTypesRepository _roomTypesRepository;

    public RoomTypesService( IRoomTypesRepository roomTypesRepository )
    {
        _roomTypesRepository = roomTypesRepository;
    }

    public async Task<OperationResult> AddAsync( RoomType roomType )
    {
        try
        {
            await _roomTypesRepository.AddAsync( roomType );

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
            await _roomTypesRepository.DeleteByIdAsync( id );

            return OperationResult.Success;
        }
        catch
        {
            return OperationResult.ServerError;
        }
    }

    public async Task<List<RoomType>?> GetAllAsync()
    {
        try
        {
            List<RoomType> roomTypes = await _roomTypesRepository.GetAllAsync();

            return roomTypes;
        }
        catch
        {
            return null;
        }
    }

    public async Task<RoomType?> GetByIdAsync( Guid id )
    {
        try
        {
            RoomType? roomType = await _roomTypesRepository.GetByIdAsync( id );

            return roomType;
        }
        catch
        {
            return null;
        }
    }

    public async Task<List<RoomType>?> GetByPropertyIdAsync( Guid propertyId )
    {
        try
        {
            List<RoomType> roomType = await _roomTypesRepository.GetByPropertyId( propertyId );

            return roomType;
        }
        catch
        {
            return null;
        }

    }

    public async Task<OperationResult> UpdateAsync( RoomType roomType )
    {
        try
        {
            await _roomTypesRepository.UpdateAsync( roomType );

            return OperationResult.Success;
        }
        catch
        {
            return OperationResult.ServerError;
        }
    }
}
