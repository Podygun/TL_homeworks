using Fighters.Extensions;
using Fighters.Models.Fighters;

namespace Fighters
{
    public class GameManager
    {
        public IFighter Play(IFighter fighterA, IFighter fighterB)
        {
            while (true)
            {
                var firstFighterDamage = fighterA.CalculateDamage();
                fighterB.TakeDamage(firstFighterDamage);
                if (!fighterB.IsAlive())
                {
                    return fighterA;
                }

                var secondFughterDamage = fighterB.CalculateDamage();
                fighterA.TakeDamage(secondFughterDamage);
                if (!fighterA.IsAlive())
                {
                    return fighterB;
                }
            }
        }
    }
}