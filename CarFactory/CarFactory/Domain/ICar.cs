namespace CarFactory.Domain;

public interface ICar
{
    public string Model { get; }
    public string BodyType { get; }
    public string Engine { get; }
    public string Transmission { get; }
    public string Color { get; }
    public string WheelPosition { get; }
    public int MaxSpeed { get; }
    public int GearCount { get; }

    public void DisplayConfiguration();
}
