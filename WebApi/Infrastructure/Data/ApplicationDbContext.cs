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

    protected override void OnModelCreating( ModelBuilder modelBuilder )
    {
        // Связь один-ко-многим между Property и RoomType
        modelBuilder.Entity<RoomType>()
            .HasOne( rt => rt.Property )
            .WithMany( p => p.RoomTypes )
            .HasForeignKey( rt => rt.PropertyId );

        // Связь многие-ко-многим между RoomType и RoomService
        modelBuilder.Entity<RoomType>()
            .HasMany( rt => rt.RoomServices )
            .WithMany( rs => rs.RoomTypes )
            .UsingEntity( j => j.ToTable( "RoomTypeRoomServices" ) );

        // Связь многие-ко-многим между RoomType и RoomAmentity
        modelBuilder.Entity<RoomType>()
            .HasMany( rt => rt.RoomAmentities )
            .WithMany( ra => ra.RoomTypes )
            .UsingEntity( j => j.ToTable( "RoomTypeRoomAmentities" ) );
    }
}
