using Application.Mappers;
using Application.Services.PropertiesServices;
using Application.Services.RoomAmentitiesServices;
using Application.Services.RoomTypesServices;
using Domain.Entities;
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace PropertiesApi
{
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
            builder.Services.AddScoped<IRoomServicesRepository, RoomServiceRepository>();
            builder.Services.AddScoped<IRoomAmentitiesRepository, RoomAmentityRepository>();
            builder.Services.AddScoped<IRoomTypesRepository, RoomTypeRepository>();

            //Services (Application)
            builder.Services.AddScoped<IPropertiesService, PropertiesService>();
            builder.Services.AddScoped<IRoomTypesService, RoomTypesService>();




            // Built-in services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
