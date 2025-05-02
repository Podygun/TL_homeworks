namespace CarFactory.Domain.BodyTypes;

internal class BaseBodyType : IBodyType
{
    public string Name { get; set; }

    public BaseBodyType( string name )
    {
        Name = name;
    }
}