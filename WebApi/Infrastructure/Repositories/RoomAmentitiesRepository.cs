﻿using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories;

public sealed class RoomAmentitiesRepository : IRoomAmentitiesRepository
{
    private readonly ApplicationDbContext _context;

    public RoomAmentitiesRepository( ApplicationDbContext context )
    {
        _context = context;
    }

    public async Task<RoomAmentity> GetByIdAsync( int id )
    {
        RoomAmentity? roomAmentity = await _context.RoomAmentities.FindAsync( id );

        if ( roomAmentity == null )
        {
            throw new InvalidOperationException( $"Room Amentity with id {id} does not exist" );
        }

        return roomAmentity;
    }

    public async Task<List<RoomAmentity>> GetAllAsync()
    {
        return await _context.RoomAmentities.ToListAsync();
    }

    public async Task AddAsync( RoomAmentity roomAmentity )
    {
        await _context.RoomAmentities.AddAsync( roomAmentity );
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync( RoomAmentity roomAmentity )
    {
        RoomAmentity? existingRoomAmentity = await GetByIdAsync( roomAmentity.Id );

        if ( existingRoomAmentity == null )
        {
            throw new InvalidOperationException( $"Room Amentity with id {roomAmentity.Id} does not exist" );
        }

        existingRoomAmentity.Name = roomAmentity.Name;
        existingRoomAmentity.RoomTypes = roomAmentity.RoomTypes;

        _context.RoomAmentities.Update( existingRoomAmentity );
        await _context.SaveChangesAsync();
    }

    public async Task DeleteByIdAsync( int id )
    {
        RoomAmentity? roomAmentity = await GetByIdAsync( id );

        if ( roomAmentity == null )
        {
            throw new InvalidOperationException( $"Property with id {id} does not exist" );
        }

        _context.RoomAmentities.Remove( roomAmentity );
        await _context.SaveChangesAsync();
    }
}
