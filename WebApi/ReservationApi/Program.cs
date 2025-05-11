
using Domain.Repositories;
using Infrastructure.Data;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ReservationApi
{
    public class Program
    {
        public static void Main( string[] args )
        {
            WebApplicationBuilder builder = WebApplication.CreateBuilder( args );

            RegisterServices( builder );

            WebApplication app = builder.Build();

            // Configure the HTTP request pipeline.
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

            // DI services
            builder.Services.AddScoped<IPropertiesRepository, PropertiesRepository>();
            builder.Services.AddScoped<IRoomServiceRepository, RoomServiceRepository>();
            builder.Services.AddScoped<IRoomAmentityRepository, RoomAmentityRepository>();
            builder.Services.AddScoped<IRoomTypeRepository, RoomTypeRepository>();

            // Built-in services
            builder.Services.AddControllers();
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
        }
    }
}
