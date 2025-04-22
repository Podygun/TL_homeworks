namespace Fighters.Models.Weapons;

public abstract class WeaponBase : IWeapon
{
    // Значения отражающие в каком процентном диапазоне
    // Может колебаться урон, относительно базового
    private const int DAMAGE_MIN_DEFLECTION = -15;
    private const int DAMAGE_MAX_DEFLECTION = 15;

    private static readonly Random _random = new();

    public abstract int Damage { get; }

    public abstract double CriticalMultiplier { get; }

    public abstract double CriticalChance { get; }

    public virtual int CalculateDamage()
    {
        bool isCritical = _random.NextDouble() < CriticalChance;
        int totalDamage = ( int )
            ( Damage * ( 1 + ( _random.Next( DAMAGE_MIN_DEFLECTION, DAMAGE_MAX_DEFLECTION + 1 ) / 100f ) ) );
        return ( int )( isCritical ? totalDamage * CriticalMultiplier : totalDamage );
    }
}
