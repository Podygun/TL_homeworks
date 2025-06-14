namespace CarFactory.Domain.Engines;

internal interface ICarEngine
{
    string Name { get; set; }

    int HorsePower { get; set; }
}
