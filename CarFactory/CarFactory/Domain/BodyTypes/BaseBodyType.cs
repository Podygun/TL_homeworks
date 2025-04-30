namespace CarFactory.Domain.BodyTypes;

internal class BaseBodyType : IBodyType
{
    protected string _name;

    public BaseBodyType( string name )
    {
        _name = name;
    }

    public string GetName()
    {
        return _name;
    }
}