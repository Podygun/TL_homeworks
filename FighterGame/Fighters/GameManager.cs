using Fighters.Data;
using Fighters.Extensions;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters;

public class GameManager
{
    private List<IFighter>? _fighters;
    private const int _MAX_FIGHTERS = 10;

    public void Run()
    {
        while ( true )
        {
            PrintMenu();
            int choice = InputInt( "Выберите действие: ", 1, 4 );

            switch ( choice )
            {
                case 1:
                    _fighters = CreateFighters();
                    break;
                case 2:
                    if ( _fighters != null || _fighters?.Count < 2 )
                    {
                        Fight( _fighters );
                    }
                    else
                    {
                        Console.WriteLine( "Сначала создайте нескольких бойцов!" );
                    }
                    break;
                case 3:
                    PrintEncyclopedia();
                    break;
                case 4:
                    return;
            }
        }
    }

    private List<IFighter> CreateFighters()
    {
        int amountFighters = InputInt( "Введите кол-во игроков", 2, _MAX_FIGHTERS );

        List<IFighter> list = new();

        for ( int i = 0; i < amountFighters; i++ )
        {
            IFighter fighter = CreateFighter();
            list.Add( fighter );
        }

        return list;
    }

    public void Fight( List<IFighter> fighters )
    {
        Console.WriteLine( "\n=== НАЧАЛО БИТВЫ ===" );
        Console.WriteLine( $"{fighter1.Name} (HP: {fighter1.GetCurrentHealth()}) vs {fighter2.Name} (HP: {fighter2.GetCurrentHealth()})\n" );

        int round = 1;

        while ( fighter1.IsAlive() && fighter2.IsAlive() )
        {
            Console.WriteLine( $"--- Раунд {round} ---" );

            int damage1 = fighter1.CalculateDamage();
            int damage2 = fighter2.CalculateDamage();

            if ( damage1 <= fighter2.CalculateArmor() )
            {
                Console.WriteLine( $"  {fighter1.Name} не может пробить {fighter2.Name}" );
                damage1 = 0;
            }

            if ( damage2 <= fighter1.CalculateArmor() )
            {
                Console.WriteLine( $"  {fighter2.Name} не может пробить {fighter1.Name}" );
                damage2 = 0;
            }

            Console.WriteLine( $"  {fighter1.Name} наносит {damage1} урона" );
            fighter2.TakeDamage( damage1 );
            Console.WriteLine( $"  {fighter2.Name} | осталось HP: {fighter2.GetCurrentHealth()}" );

            if ( !fighter2.IsAlive() ) break;

            // Атака второго бойца
            Console.WriteLine( $"\n  {fighter2.Name} наносит {damage2} урона" );
            fighter1.TakeDamage( damage2 );
            Console.WriteLine( $"  {fighter1.Name} | осталось HP: {fighter1.GetCurrentHealth()}" );

            round++;
            Console.WriteLine();
        }

        // Определение победителя
        IFighter winner = fighter1.IsAlive() ? fighter1 : fighter2;
        IFighter loser = fighter1.IsAlive() ? fighter2 : fighter1;

        Console.WriteLine( "\n=== БИТВА ЗАВЕРШЕНА ===" );
        Console.WriteLine( $"{loser.Name} погибает" );
        Console.WriteLine( $"{winner.Name} побеждает с {winner.GetCurrentHealth()} HP!" );

        ResetFightersState(fighters);
    }

    private IFighter CreateFighter()
    {
        string name = InputString( "Введите имя бойца: " );
        IRace race = ChooseRace();
        WeaponBase weapon = ChooseWeapon();
        IArmor armor = ChooseArmor();
        return ChooseFighterClass( name, race, weapon, armor );
    }

    private IFighter ChooseFighterClass( string name, IRace race, WeaponBase weapon, IArmor armor )
    {
        Console.WriteLine( "\nВыберите класс:" );
        List<string> classes = GameItems.FighterClasses.Keys.ToList();

        for ( int i = 0; i < classes.Count; i++ )
        {
            Console.WriteLine( $"{i + 1}. {classes[ i ]}" );
        }

        int choice = InputInt( "> ", 1, classes.Count );
        string selectedClass = classes[ choice - 1 ];

        return GameItems.FighterClasses[ selectedClass ]( name, race, weapon, armor );
    }

    private IRace ChooseRace()
    {
        Console.WriteLine( "\nВыберите расу:" );
        List<IRace> races = GameItems.Races;
        for ( int i = 0; i < races.Count; i++ )
        {
            IRace race = races[ i ];
            Console.WriteLine( $"{i + 1}. {race.GetType().Name.Replace( "Race", "" )} " +
                             $"(Урон: {race.Damage}, Здоровье: {race.Health}, Броня: {race.Armor})" );
        }

        int choice = InputInt( "> ", 1, GameItems.Races.Count );
        return GameItems.Races[ choice - 1 ];
    }

    private WeaponBase ChooseWeapon()
    {
        Console.WriteLine( "\nВыберите оружие:" );
        List<WeaponBase> weapons = GameItems.Weapons;

        for ( int i = 0; i < weapons.Count; i++ )
        {
            WeaponBase weapon = weapons[ i ];
            Console.WriteLine( $"{i + 1}. {weapon.GetType().Name} (Урон: {weapon.Damage})" );
        }

        int choice = InputInt( "> ", 1, GameItems.Weapons.Count );
        return GameItems.Weapons[ choice - 1 ];
    }

    private IArmor ChooseArmor()
    {
        Console.WriteLine( "\nВыберите броню:" );
        List<IArmor> armors = GameItems.Armors;

        for ( int i = 0; i < armors.Count; i++ )
        {
            IArmor armor = armors[ i ];
            Console.WriteLine( $"{i + 1}. {armor.GetType().Name} (Защита: {armor.Armor})" );
        }

        int choice = InputInt( "> ", 1, GameItems.Armors.Count );
        return GameItems.Armors[ choice - 1 ];
    }

    private void PrintMenu()
    {
        Console.WriteLine( "\n=== Меню (вводите соответствующие цифры)===" );
        Console.WriteLine( "1. Добавить бойцов" );
        Console.WriteLine( "2. Начать битву" );
        Console.WriteLine( "3. Энциклопедия" );
        Console.WriteLine( "4. Выход" );
        Console.WriteLine( "============" );
    }

    private void PrintEncyclopedia()
    {
        Console.WriteLine( "╔════════════════════════════════════╗" );
        Console.WriteLine( "║         ЭНЦИКЛОПЕДИЯ ИГРЫ          ║" );
        Console.WriteLine( "╚════════════════════════════════════╝\n" );

        Console.WriteLine( "\n\tРАСЫ" );
        foreach ( var race in GameItems.Races )
        {
            Console.WriteLine( $"{race.GetType().Name}" );
            Console.WriteLine( $"  Здоровье: {race.Health}" );
            Console.WriteLine( $"  Урон:     {race.Damage}" );
            Console.WriteLine( $"  Броня:    {race.Armor}" );
        }

        Console.WriteLine( "\n\tОРУЖИЕ" );
        foreach ( var weapon in GameItems.Weapons )
        {
            Console.WriteLine( $"{weapon.GetType().Name}" );
            Console.WriteLine( $"  Базовый урон:    {weapon.Damage}" );
            Console.WriteLine( $"  Крит. шанс:      {weapon.CriticalChance:P0}" );
            Console.WriteLine( $"  Множитель крита: {weapon.CriticalMultiplier}x" );
        }

        Console.WriteLine( "\n\tБРОНЯ" );
        foreach ( var armor in GameItems.Armors )
        {
            Console.WriteLine( $"{armor.GetType().Name}" );
            Console.WriteLine( $"  Защита: {armor.Armor}\n" );
        }

        Console.WriteLine( "\n\tКЛАССЫ" );
        //foreach ( var item in GameItems.FighterClasses )
        //{
        //    Console.WriteLine( $"┌ {item.}" );
        //    Console.WriteLine( $"├ Описание: {instance.GetDescription()}" );
        //    Console.WriteLine( $"└ Особенности:" );

        //    if ( type == typeof( OrcBerserker ) )
        //        Console.WriteLine( "   - Снижение урона на 20%" );
        //    else if ( type == typeof( ElfArcher ) )
        //        Console.WriteLine( "   - Повышенный шанс крита" );
        //    else
        //        Console.WriteLine( "   - Стандартные характеристики" );

        //    Console.WriteLine();
        //}
    }

    private string InputString( string message )
    {
        string input;
        do
        {
            Console.Write( message );
            input = Console.ReadLine()?.Trim();
            if ( string.IsNullOrEmpty( input ) )
            {
                Console.WriteLine( "Ошибка: пустая строка, повторите ввод." );
            }
        } while ( string.IsNullOrEmpty( input ) );

        return input;
    }

    private int InputInt( string message, int min, int max )
    {
        while ( true )
        {
            Console.Write( message );
            if ( int.TryParse( Console.ReadLine(), out int number ) && number >= min && number <= max )
            {
                return number;
            }
            Console.WriteLine( $"Ошибка: введите число от {min} до {max}!" );
        }
    }

    private void ResetFightersState( List<IFighter> fighters )
    {
        foreach ( var fighter in fighters )
        {
            fighter.ResetState();
        }
    }

}