namespace Fighters.Models.Fighters;

public abstract class FighterBase : IFighter
{
    public virtual string Name { get; protected set; }
    protected int _currentHealth;
    protected readonly int _maxHealth;

    protected FighterBase( string name, int maxHealth )
    {
        Name = name;
        _maxHealth = maxHealth;
        _currentHealth = _maxHealth;
    }

    public abstract int CalculateArmor();
    public abstract int CalculateDamage();
    public abstract string GetDescription();

    public virtual int GetCurrentHealth() => _currentHealth;
    public virtual int GetMaxHealth() => _maxHealth;
    public virtual bool IsAlive() => _currentHealth > 0;

    public virtual void ResetState()
    {
        _currentHealth = _maxHealth;
    }

    public virtual void TakeDamage( int damage )
    {
        int totalDamage = Math.Max( damage - CalculateArmor(), 0 );
        _currentHealth = Math.Max( _currentHealth - totalDamage, 0 );
    }
}
