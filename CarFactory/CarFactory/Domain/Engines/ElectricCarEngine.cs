namespace CarFactory.Domain.Engines;

internal sealed class ElectricCarEngine : BaseCarEngine
{
    public ElectricCarEngine() : base( Localizator.ElectricEngine, 550 ) { }
}