namespace CarFactory.Domain.Engines;

internal sealed class PetrolCarEngine : BaseCarEngine
{
    public PetrolCarEngine() : base( Localizator.PetrolEngine, 450 ) { }
}