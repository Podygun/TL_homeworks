using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Berserker : IFighter
{
    private readonly IRace _race;
    private readonly IArmor _armor;
    private readonly IWeapon _weapon;

    private int _initialHealth;
    private int _currentHealth;

    public string Name { get; private set; }

    public Berserker( string name, IRace race, IWeapon weapon, IArmor armor )
    {
        Name = name;
        _race = race;
        _weapon = weapon;
        _armor = armor;
        _initialHealth = GetMaxHealth();
        _currentHealth = _initialHealth;
    }

    public string GetDescription() =>
        "Бешеный воин, игнорирующий часть урона. " +
        $"Способность: снижает входящий урон на 20%. ";

    public int GetCurrentHealth() => _currentHealth;

    public int GetMaxHealth() => _race.Health;

    public int CalculateDamage() => _weapon.Damage + _race.Damage;

    public int CalculateArmor() => _armor.Armor + _race.Armor;

    public void TakeDamage( int damage )
    {
        int berserkerReduction = ( int )( damage * 0.2 );
        int totalDamage = Math.Max( damage - berserkerReduction - CalculateArmor(), 0 );
        _currentHealth = Math.Max( _currentHealth - totalDamage, 0 );
    }

    public void ResetState()
    {
        _currentHealth = _initialHealth;
    }
}
