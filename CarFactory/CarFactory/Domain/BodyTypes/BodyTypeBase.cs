namespace CarFactory.Domain.BodyTypes;

internal class BodyTypeBase : IBodyType
{
    protected string _name;

    public BodyTypeBase( string name )
    {
        _name = name;
    }

    public string GetName() => _name;
}