using Fighters.Models.Armors;
using Fighters.Models.Races;
using Fighters.Models.Weapons;
using Moq;

namespace Figters.Tests.Models.FightersTests;

public abstract class BaseMockFighter
{
    protected int _fighterHealth = 100;
    protected int _fighterArmor = 5;
    protected int _fighterRaceDamage = 5;
    protected int _weaponDamage = 20;
    protected int _raceArmor = 10;

    protected Mock<IRace> _mockRace;
    protected Mock<IArmor> _mockArmor;
    protected Mock<WeaponBase> _mockWeapon;

    protected BaseMockFighter()
    {
        _mockRace = new Mock<IRace>();
        _mockArmor = new Mock<IArmor>();
        _mockWeapon = new Mock<WeaponBase>();

        _mockRace.Setup( r => r.Health ).Returns( _fighterHealth );
        _mockRace.Setup( r => r.Damage ).Returns( _fighterRaceDamage );
        _mockRace.Setup( r => r.Armor ).Returns( _raceArmor );
        _mockArmor.Setup( a => a.Armor ).Returns( _fighterArmor );
        _mockWeapon.Setup( w => w.CalculateDamage() ).Returns( _weaponDamage );
        _mockWeapon.Setup( w => w.Damage ).Returns( _weaponDamage );
    }
}
