using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Moq;

namespace Figters.Tests.Sevices.GameManagerTests
{
    public class ResetFightersTests
    {
        [Fact]
        public void ResetFightersState_ShouldHandleEmptyList()
        {
            // Arrange
            var fighters = new List<FighterBase>();

            // Act & Assert (не должно быть исключений)
            MockGameManager.PublicResetFightersState( fighters );
        }

        [Fact]
        public void ResetFightersState_ShouldHandleNullFightersList()
        {
            // Arrange
            List<FighterBase>? fighters = null;

            // Act & Assert
            Assert.Throws<NullReferenceException>( () =>
                MockGameManager.PublicResetFightersState( fighters ) );
        }

        [Fact]
        public void ResetFightersState_ShouldResetHealthToMaxForAllFighters()
        {
            // Arrange
            (Mock<IRace> raceMock, Mock<IArmor> armorMock, Mock<WeaponBase> weaponMock) = CreateMocks();

            Berserker berserker1 = new Berserker( "Berserker1", raceMock.Object, weaponMock.Object, armorMock.Object );
            Berserker berserker2 = new Berserker( "Berserker2", raceMock.Object, weaponMock.Object, armorMock.Object );

            // Повреждаем бойцов
            berserker1.TakeDamage( 30 );
            berserker2.TakeDamage( 50 );

            List<FighterBase> fighters = new List<FighterBase> { berserker1, berserker2 };

            // Act
            MockGameManager.PublicResetFightersState( fighters );

            // Assert
            Assert.Equal( 100, berserker1.GetCurrentHealth() );
            Assert.Equal( 100, berserker2.GetCurrentHealth() );
        }

        private (Mock<IRace>, Mock<IArmor>, Mock<WeaponBase>) CreateMocks()
        {
            Mock<IRace> raceMock = new Mock<IRace>();
            raceMock.Setup( r => r.Health ).Returns( 100 );

            Mock<IArmor> armorMock = new Mock<IArmor>();
            Mock<WeaponBase> weaponMock = new Mock<WeaponBase>();

            return (raceMock, armorMock, weaponMock);
        }
    }
}
