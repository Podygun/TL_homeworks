namespace CarFactory.Domain.Transmissions;

internal interface ITransmission
{
    public string GetName();

    public int GetGearsAmount();
}