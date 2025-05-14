using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Models.Fighters;

public class Berserker : FighterBase
{
    private readonly IRace _race;
    private readonly IArmor _armor;
    private readonly WeaponBase _weapon;

    public Berserker( string name, IRace race, WeaponBase weapon, IArmor armor )
        : base( name, race.Health )
    {
        _race = race;
        _weapon = weapon;
        _armor = armor;
    }

    public override string GetDescription() =>
        "Бешеный воин, игнорирующий часть урона. " +
        $"Способность: снижает входящий урон на 20%. ";

    public override int CalculateDamage() => _weapon.Damage + _race.Damage;

    public override int CalculateArmor() => _armor.Armor + _race.Armor;

    public override void TakeDamage( int damage )
    {
        double damageReduction = damage * 0.2;
        int totalDamage = ( int )( damage - damageReduction - CalculateArmor() );
        base.TakeDamage( totalDamage );
    }
}
