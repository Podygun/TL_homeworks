namespace CarFactory.Domain.Transmissions;

internal sealed class AutomaticTransmission : BaseTransmission
{
    public AutomaticTransmission() : base( Localizator.AutomaticTransmission, 8 ) { }
}