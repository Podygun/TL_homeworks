using Fighters.Models.Fighters;

namespace Figters.Tests.Models.FightersTests.BaseFighterTests;

public sealed class TestFighter : FighterBase
{
    public TestFighter( string name, int maxHealth ) : base( name, maxHealth ) { }

    public override int CalculateArmor() => 0;
    public override int CalculateDamage() => 0;
    public override string GetDescription() => "Тестовый боец";
}
