using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public sealed class ApplicationDbContext : DbContext
{
    public ApplicationDbContext( DbContextOptions<ApplicationDbContext> options )
        : base( options )
    {
    }

    public DbSet<Property> Properties { get; set; }
    public DbSet<RoomAmentity> RoomAmentities { get; set; }
    public DbSet<RoomService> RoomServices { get; set; }
    public DbSet<RoomType> RoomTypes { get; set; }
    public DbSet<Reservation> Reservations { get; set; }

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        // Конфигурация RoomType
        modelBuilder.Entity<RoomType>( entity =>
        {
            // Связь с Property
            entity.HasOne( rt => rt.Property )
                .WithMany( p => p.RoomTypes )
                .HasForeignKey( rt => rt.PropertyId );

            // Связь с Reservation
            entity.HasMany( rt => rt.Reservations )
                .WithOne( r => r.RoomType )
                .HasForeignKey( r => r.RoomTypeId )
                .OnDelete( DeleteBehavior.Restrict );

            // Связи многие-ко-многим
            entity.HasMany( rt => rt.RoomServices )
                .WithMany( rs => rs.RoomTypes )
                .UsingEntity( j => j.ToTable( "RoomTypeRoomServices" ) );

            entity.HasMany( rt => rt.RoomAmentities )
                .WithMany( ra => ra.RoomTypes )
                .UsingEntity( j => j.ToTable( "RoomTypeRoomAmentities" ) );
        } );

        // Конфигурация Reservation
        modelBuilder.Entity<Reservation>( entity =>
        {
            entity.HasOne( r => r.Property )
                .WithMany( p => p.Reservations )
                .HasForeignKey( r => r.PropertyId )
                .OnDelete( DeleteBehavior.Restrict );

            entity.HasOne( r => r.RoomType )
                .WithMany( rt => rt.Reservations )
                .HasForeignKey( r => r.RoomTypeId )
                .OnDelete( DeleteBehavior.Restrict );

            entity.HasIndex( r => r.PropertyId );
            entity.HasIndex( r => r.RoomTypeId );
            entity.HasIndex( r => r.ArrivalDateTime );
            entity.HasIndex( r => r.DepartureDateTime );
        } );
    }
}
