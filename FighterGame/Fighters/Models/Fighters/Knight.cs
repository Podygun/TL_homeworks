using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Knight : IFighter
{
    private readonly IRace _race;
    private readonly IArmor _armor;
    private readonly WeaponBase _weapon;

    private int _initialHealth;
    private int _currentHealth;

    public string Name { get; private set; }

    public Knight( string name, IRace race, WeaponBase weapon, IArmor armor )
    {
        Name = name;
        _race = race;
        _weapon = weapon;
        _armor = armor;
        _initialHealth = GetMaxHealth();
        _currentHealth = _initialHealth;
    }

    public int GetCurrentHealth() => _currentHealth;

    public int GetMaxHealth() => _race.Health;

    public int CalculateDamage() => _weapon.CalculateDamage() + _race.Damage;

    public int CalculateArmor() => _armor.Armor + _race.Armor;

    public string GetDescription() =>
        "Базовый воин с небольшой стойкостью к урону";

    public void TakeDamage( int damage )
    {
        double damageReduction = damage * 0.1;
        double totalDamage = Math.Max( damage - damageReduction - CalculateArmor(), 0 );

        int value = ( int )Math.Max( _currentHealth - totalDamage, 0 );
        _currentHealth = value;
    }

    public void ResetState()
    {
        _currentHealth = _initialHealth;
    }
}