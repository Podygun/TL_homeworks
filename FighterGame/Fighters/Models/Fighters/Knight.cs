using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Knight : FighterBase
{
    private readonly IRace _race;
    private readonly IArmor _armor;
    private readonly WeaponBase _weapon;

    private int _initialHealth;
    private int _currentHealth;

    public override string Name { get; protected set; }

    public Knight( string name, IRace race, WeaponBase weapon, IArmor armor )
    {
        Name = name;
        _race = race;
        _weapon = weapon;
        _armor = armor;
        _initialHealth = GetMaxHealth();
        _currentHealth = _initialHealth;
    }

    public override int GetCurrentHealth() => _currentHealth;

    public override int GetMaxHealth() => _race.Health;

    public override int CalculateDamage() => _weapon.CalculateDamage() + _race.Damage;

    public override int CalculateArmor() => _armor.Armor + _race.Armor;

    public override string GetDescription() =>
        "Базовый воин с небольшой стойкостью к урону";

    public override void TakeDamage( int damage )
    {
        double damageReduction = damage * 0.1;
        double totalDamage = Math.Max( damage - damageReduction - CalculateArmor(), 0 );

        int value = ( int )Math.Max( _currentHealth - totalDamage, 0 );
        _currentHealth = value;
    }

    public override void ResetState()
    {
        _currentHealth = _initialHealth;
    }
}