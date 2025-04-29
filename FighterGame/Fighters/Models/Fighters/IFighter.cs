namespace Fighters.Models.Fighters;

public interface IFighter
{
    public string GetDescription();
    public int GetCurrentHealth();
    public int GetMaxHealth();
    public int CalculateDamage();
    public int CalculateArmor();
    public void ResetState();
    public void TakeDamage( int damage );
    public bool IsAlive();
}