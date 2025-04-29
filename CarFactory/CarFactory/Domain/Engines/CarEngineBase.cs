namespace CarFactory.Domain.Engines;

internal class CarEngineBase : ICarEngine
{
    protected string _name;
    protected int _horsePower;

    protected CarEngineBase( string name, int horsePower )
    {
        _name = name;
        _horsePower = horsePower;
    }

    public string GetName() => _name;

    public int GetHorsePower() => _horsePower;
}
