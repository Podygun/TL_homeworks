using Fighters;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Figters.Tests.Sevices.GameManagerTests;

public class MockGameManager : GameManager
{
    public static string InputString( string message )
    {
        return GameManager.InputString( message );
    }

    public static int InputInt( string message, int min, int max )
    {
        return GameManager.InputInt( message, min, max );
    }

    protected override IRace ChooseRace() => new Human();
    protected override WeaponBase ChooseWeapon() => new Fists();
    protected override IArmor ChooseArmor() => new NoArmor();

    public FighterBase CreateFighter( string name, IRace race, WeaponBase weapon, IArmor armor )
    {
        return new Knight( name, race, weapon, armor );
    }

    public static void PublicResetFightersState( List<FighterBase> fighters )
    {
        ResetFightersState( fighters );
    }
}