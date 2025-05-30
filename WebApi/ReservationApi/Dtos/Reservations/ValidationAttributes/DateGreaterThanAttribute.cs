using System.ComponentModel.DataAnnotations;

namespace ReservationApi.Dtos.Reservations.ValidationAttributes;

public class DateGreaterThanAttribute : ValidationAttribute
{
    private readonly string _comparisonProperty;

    public DateGreaterThanAttribute( string comparisonProperty )
    {
        _comparisonProperty = comparisonProperty;
    }

    protected override ValidationResult IsValid( object value, ValidationContext validationContext )
    {
        DateOnly currentValue = ( DateOnly )value;
        System.Reflection.PropertyInfo? property = validationContext.ObjectType.GetProperty( _comparisonProperty );

        if ( property == null )
        {
            throw new ArgumentException( "Свойство для сравнения не найдено" );
        }

        DateOnly comparisonValue = ( DateOnly )property.GetValue( validationContext.ObjectInstance );

        if ( currentValue > comparisonValue )
        {
            return ValidationResult.Success;
        }

        return new ValidationResult( ErrorMessage );
    }
}
