using System.ComponentModel.DataAnnotations;
using ReservationApi.Dtos.Reservations.ValidationAttributes;

namespace ReservationApi.Dtos.Reservations;

public class CreateReservationDto
{
    [Range( 1, int.MaxValue )]
    public int PropertyId { get; set; }


    [Range( 1, int.MaxValue )]
    public int RoomTypeId { get; set; }


    [Required( ErrorMessage = "Дата заезда обязательна" )]
    [FutureDate( ErrorMessage = "Дата заезда должна быть не раньше текущей даты" )]
    [DataType( DataType.Date )]
    public DateOnly ArrivalDate { get; set; }


    [Required( ErrorMessage = "Дата выезда обязательна" )]
    [FutureDate( ErrorMessage = "Дата выезда должна быть в будущем" )]
    [DateGreaterThan( nameof( ArrivalDate ), ErrorMessage = "Дата выезда должна быть после даты заезда" )]
    [DataType( DataType.Date )]
    public DateOnly DepartureDate { get; set; }


    [Required( ErrorMessage = "Имя гостя обязательно" )]
    [StringLength( 50, MinimumLength = 2, ErrorMessage = "Имя должно быть от 2 до 50 символов" )]
    [RegularExpression( @"^[\p{L}\s'-]+$", ErrorMessage = "Имя содержит недопустимые символы" )]
    public string GuestName { get; set; }


    [Required( ErrorMessage = "Номер телефона обязателен" )]
    [StringLength( maximumLength: 18, MinimumLength = 6 )]
    [Phone( ErrorMessage = "Неверный формат телефона" )]
    public string GuestPhoneNumber { get; set; }
}