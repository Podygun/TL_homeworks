namespace CarFactory.Domain.Transmissions;

internal abstract class BaseTransmission : ITransmission
{
    protected string _name;
    protected int _gears;

    protected BaseTransmission( string name, int gears )
    {
        _name = name;
        _gears = gears;
    }

    public int GetGearsAmount()
    {
        return _gears;
    }

    public string GetName()
    {
        return _name;
    }
}