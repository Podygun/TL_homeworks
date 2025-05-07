namespace CarFactory.Domain.Engines;

internal sealed class DieselCarEngine : BaseCarEngine
{
    public DieselCarEngine() : base( Localizator.DieselEngine, 300 ) { }
}