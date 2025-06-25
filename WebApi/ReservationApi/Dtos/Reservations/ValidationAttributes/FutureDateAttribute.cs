using System.ComponentModel.DataAnnotations;

namespace ReservationApi.Dtos.Reservations.ValidationAttributes;

public class FutureDateAttribute : ValidationAttribute
{
    public override bool IsValid( object value )
    {
        if ( value is not DateOnly date )
            return false;

        return date > DateOnly.FromDateTime( DateTime.Now );
    }
}
