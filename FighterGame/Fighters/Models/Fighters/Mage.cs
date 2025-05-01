using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Mage : FighterBase
{
    private readonly IRace _race;
    private readonly IArmor _armor;
    private readonly IWeapon _weapon;

    public Mage( string name, IRace race, WeaponBase weapon, IArmor armor )
        : base( name, race.Health )
    {
        _race = race;
        _weapon = weapon;
        _armor = armor;
    }

    public override string GetDescription() =>
        "Большой урон, малое здоровье . " +
        $"Способность: нет ";

    public override int CalculateDamage() => _weapon.Damage + _race.Damage;

    public override int CalculateArmor() => _armor.Armor + _race.Armor;

    public override void TakeDamage( int damage )
    {
        int totalDamage = damage - CalculateArmor();
        base.TakeDamage( totalDamage );
    }
}
