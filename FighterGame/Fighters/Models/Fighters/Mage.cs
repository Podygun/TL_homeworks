using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Mage : FighterBase
{
    private readonly IRace _race;
    private readonly IArmor _armor;
    private readonly IWeapon _weapon;

    private int _initialHealth;
    private int _currentHealth;

    public override string Name { get; protected set; }

    public Mage( string name, IRace race, IWeapon weapon, IArmor armor )
    {
        Name = name;
        _race = race;
        _weapon = weapon;
        _armor = armor;
        _initialHealth = GetMaxHealth();
        _currentHealth = _initialHealth;
    }

    public override string GetDescription() =>
        "Большой урон, малое здоровье . " +
        $"Способность: нет ";

    public override int GetCurrentHealth() => _currentHealth;

    public override int GetMaxHealth() => _race.Health;

    public override int CalculateDamage() => _weapon.Damage + _race.Damage;

    public override int CalculateArmor() => _armor.Armor + _race.Armor;

    public override void TakeDamage( int damage )
    {
        int totalDamage = Math.Max( damage - CalculateArmor(), 0 );
        _currentHealth = Math.Max( _currentHealth - totalDamage, 0 );
    }

    public override void ResetState()
    {
        _currentHealth = _initialHealth;
    }
}
