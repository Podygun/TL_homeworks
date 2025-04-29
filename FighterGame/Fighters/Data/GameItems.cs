using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters.Data;

public static class GameItems
{
    public static readonly IReadOnlyList<IRace> Races = new List<IRace>
    {
        new Human(),
        new Dwarf(),
        new Elf(),
        new Orc(),
    };

    public static readonly IReadOnlyList<WeaponBase> Weapons = new List<WeaponBase>()
    {
        new Fists(),
        new Bow(),
        new BattleAxe(),
        new Warhammer()
    };

    public static readonly IReadOnlyList<IArmor> Armors = new List<IArmor>()
    {
        new NoArmor(),
        new OldRobe(),
        new LeatherArmor(),
        new IronArmor()
    };

    public static readonly Dictionary<string, Func<string, IRace, WeaponBase, IArmor, FighterBase>> FighterClasses = new()
    {
        [ "Рыцарь" ] = ( name, race, weapon, armor ) => new Knight( name, race, weapon, armor ),
        [ "Берсерк" ] = ( name, race, weapon, armor ) => new Berserker( name, race, weapon, armor ),
        [ "Маг" ] = ( name, race, weapon, armor ) => new Mage( name, race, weapon, armor )
    };
}
