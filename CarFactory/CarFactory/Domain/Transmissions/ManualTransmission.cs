namespace CarFactory.Domain.Transmissions;

internal sealed class ManualTransmission : BaseTransmission
{
    public ManualTransmission() : base( Localizator.ManualTransmission, 6 ) { }
}