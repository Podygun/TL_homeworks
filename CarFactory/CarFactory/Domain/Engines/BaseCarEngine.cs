namespace CarFactory.Domain.Engines;

internal class BaseCarEngine : ICarEngine
{
    public string Name { get; set; }
    public int HorsePower { get; set; }

    protected BaseCarEngine( string name, int horsePower )
    {
        Name = name;
        HorsePower = horsePower;
    }
}
