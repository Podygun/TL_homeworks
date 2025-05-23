using Application.Services.PropertiesServices;
using Application.Services.RoomTypesServices;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PropertiesApi;

public class Program
{
    public static void Main( string[] args )
    {
        WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

        RegisterServices( builder );

        WebApplication app = builder.Build();

        if ( app.Environment.IsDevelopment() )
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();
        app.MapControllers();

        app.Run();
    }

    public static void RegisterServices( WebApplicationBuilder builder )
    {
        // SQLite database connection
        builder.Services.AddDbContext<ApplicationDbContext>( options =>
            options.UseSqlite( builder.Configuration.GetConnectionString( "DefaultConnection" ) ) );

        // Repos (Infrastructure)
        builder.Services.AddScoped<IPropertiesRepository, PropertiesRepository>();
        builder.Services.AddScoped<IRoomServicesRepository, RoomServicesRepository>();
        builder.Services.AddScoped<IRoomAmentitiesRepository, RoomAmentitiesRepository>();
        builder.Services.AddScoped<IRoomTypesRepository, RoomTypesRepository>();

        //Services (Application)
        builder.Services.AddScoped<IPropertiesService, PropertiesService>();
        builder.Services.AddScoped<IRoomTypesService, RoomTypesService>();

        // Built-in services
        builder.Services.AddControllers();
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
    }
}
