using Domain.Entities;
using ReservationApi.Dtos.Properties;
using ReservationApi.Dtos.Reservations;
using ReservationApi.Dtos.RoomTypes;

namespace ReservationApi.Mappers;

internal static class ReservationMapper
{
    internal static List<Reservation> ToDomain( this List<CreateReservationDto> dtoList )
    {
        return dtoList.Select( d => new Reservation()
        {
            PropertyId = d.PropertyId,
            RoomTypeId = d.RoomTypeId,
            ArrivalDateTime = d.ArrivalDate.ToDateTime( TimeOnly.MinValue ),
            DepartureDateTime = d.DepartureDate.ToDateTime( TimeOnly.MinValue ),
            GuestName = d.GuestName,
            GuestPhoneNumber = d.GuestPhoneNumber
        } ).ToList();
    }

    internal static Reservation ToDomain( this CreateReservationDto dto )
    {
        return new Reservation()
        {
            PropertyId = dto.PropertyId,
            RoomTypeId = dto.RoomTypeId,
            ArrivalDateTime = dto.ArrivalDate.ToDateTime( TimeOnly.MinValue ),
            DepartureDateTime = dto.DepartureDate.ToDateTime( TimeOnly.MinValue ),
            GuestName = dto.GuestName,
            GuestPhoneNumber = dto.GuestPhoneNumber
        };
    }

    internal static ReservationDto ToDto( this Reservation domain )
    {
        return new ReservationDto()
        {
            ArrivalDateTime = domain.ArrivalDateTime,
            DepartureDateTime = domain.DepartureDateTime,
            GuestName = domain.GuestName,
            GuestPhoneNumber = domain.GuestPhoneNumber,
            Currency = domain.Currency,
            Total = domain.Total,
            Property = new PropertyDto()
            {
                Id = domain.Property.Id,
                Address = domain.Property.Address,
                City = domain.Property.City,
                Country = domain.Property.Country,
                Latitude = domain.Property.Latitude,
                Longitude = domain.Property.Longitude,
                Name = domain.Property.Name
            },
            RoomType = new RoomTypeDto()
            {
                Currency = domain.RoomType.Currency,
                DailyPrice = domain.RoomType.DailyPrice,
                Id = domain.Property.Id,
                MaxPersonCount = domain.RoomType.MaxPersonCount,
                MinPersonCount = domain.RoomType.MinPersonCount,
            }
        };
    }

    internal static List<ReservationDto> ToDto( this List<Reservation> dtoList )
    {
        return dtoList.Select( dto => dto.ToDto() ).ToList();
    }

}
