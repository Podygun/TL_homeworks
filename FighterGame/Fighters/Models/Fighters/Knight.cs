using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Knight : FighterBase
{
    private readonly IRace _race;
    private readonly IArmor _armor;
    private readonly WeaponBase _weapon;

    public Knight( string name, IRace race, WeaponBase weapon, IArmor armor )
        : base( name, race.Health )
    {
        _race = race;
        _weapon = weapon;
        _armor = armor;
    }

    public override int CalculateDamage() => _weapon.CalculateDamage() + _race.Damage;

    public override int CalculateArmor() => _armor.Armor + _race.Armor;

    public override string GetDescription() =>
        "Базовый воин с небольшой стойкостью к урону";

    public override void TakeDamage( int damage )
    {
        double damageReduction = damage * 0.1;
        int totalDamage = ( int )( damage - damageReduction - CalculateArmor() );
        base.TakeDamage( totalDamage );
    }
}