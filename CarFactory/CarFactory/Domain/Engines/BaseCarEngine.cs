namespace CarFactory.Domain.Engines;

internal class BaseCarEngine : ICarEngine
{
    protected string _name;
    protected int _horsePower;

    protected BaseCarEngine( string name, int horsePower )
    {
        _name = name;
        _horsePower = horsePower;
    }

    public string GetName()
    {
        return _name;
    }

    public int GetHorsePower()
    {
        return _horsePower;
    }
}
