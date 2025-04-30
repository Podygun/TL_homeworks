namespace CarFactory.Domain.Transmissions;

internal interface ITransmission
{
    string GetName();

    int GetGearsAmount();
}