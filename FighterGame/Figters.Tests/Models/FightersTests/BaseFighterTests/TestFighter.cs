using Fighters.Models.Fighters;

namespace Figters.Tests.Models.FightersTests.BaseFighterTests
{
    public sealed class TestFighter : FighterBase
    {
        public TestFighter( string name, int maxHealth ) : base( name, maxHealth ) { }

        public override int CalculateArmor() => 0; // Реализация абстрактного метода
        public override int CalculateDamage() => 0; // Реализация абстрактного метода
        public override string GetDescription() => "Тестовый боец"; // Реализация абстрактного
    }
}
