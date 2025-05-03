using Fighters.Data;
using Fighters.Models.Armors;
using Fighters.Models.Fighters;
using Fighters.Models.Races;
using Fighters.Models.Weapons;

namespace Fighters;

public class GameManager
{
    private List<FighterBase>? _fighters;
    private const int MaxFightersAmount = 10;

    public void Run()
    {
        while ( true )
        {
            PrintMenu();
            int choice = InputInt( "Выберите действие: ", 1, 5 );

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
                    _fighters = CreateRandomFighters();
                    Fight( _fighters );
                    break;
                case 5:
                    return;
                default:
                    Console.WriteLine( "Неизвестная команда" );
                    break;
            }
        }
    }

    protected List<FighterBase> CreateFighters()
    {
        int amountFighters = InputInt( "Введите кол-во игроков ", 2, MaxFightersAmount );

        List<FighterBase> list = new();

        for ( int i = 0; i < amountFighters; i++ )
        {
            FighterBase fighter = CreateFighter();
            list.Add( fighter );
        }

        return list;
    }

    protected void Fight( List<FighterBase> fighters )
    {
        Console.WriteLine( "\n=== НАЧАЛО БИТВЫ ===" );

        // Выводим список бойцов для выбора инициативы
        Console.WriteLine( "Выберите бойца, который ходит первым:" );
        for ( int i = 0; i < fighters.Count; i++ )
        {
            Console.WriteLine( $"{i + 1}. {fighters[ i ].Name} (HP: {fighters[ i ].GetCurrentHealth()})" );
        }

        // Выбор инициативы
        int currentAttackerIndex = InputInt( "> ", 1, fighters.Count ) - 1;

        int round = 1;
        bool isBattleEnded = false;

        while ( !isBattleEnded )
        {
            // Находим следующего живого бойца
            int attempts = 0;
            while ( !fighters[ currentAttackerIndex ].IsAlive() && attempts < fighters.Count )
            {
                currentAttackerIndex = ( currentAttackerIndex + 1 ) % fighters.Count;
                attempts++;
            }

            // Если все бойцы мертвы
            if ( attempts >= fighters.Count )
            {
                Console.WriteLine( "Все бойцы мертвы! Ничья!" );
                break;
            }

            FighterBase attacker = fighters[ currentAttackerIndex ];
            Console.WriteLine( $"\n--- Раунд {round} ---\n" );
            Console.WriteLine( $"Ходит: {attacker.Name}" );

            bool idMadeAttack = false;

            // Атака всех остальных живых бойцов
            for ( int i = 0; i < fighters.Count; i++ )
            {
                if ( i == currentAttackerIndex || !fighters[ i ].IsAlive() ) continue;

                FighterBase defender = fighters[ i ];

                int damage = attacker.CalculateDamage();
                int armor = defender.CalculateArmor();

                if ( damage <= armor )
                {
                    Console.WriteLine( $"  {attacker.Name} не может пробить {defender.Name}" );
                    damage = 0;
                }

                Console.WriteLine( $"Он атакует {defender.Name} и наносит {damage} урона" );
                defender.TakeDamage( damage );
                Console.WriteLine( $"\t( У {defender.Name} осталось {defender.GetCurrentHealth()} HP)" );

                idMadeAttack = true;

                if ( !defender.IsAlive() )
                {
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine( $"{defender.Name} погибает!" );
                    Console.ResetColor();

                    // Проверяем, не остался ли только один живой боец
                    if ( fighters.Count( f => f.IsAlive() ) == 1 )
                    {
                        isBattleEnded = true;
                        break;
                    }
                }
            }

            // Переход хода к следующему бойцу
            currentAttackerIndex = ( currentAttackerIndex + 1 ) % fighters.Count;

            // Увеличиваем счетчик раундов только если была совершена хотя бы одна атака
            if ( idMadeAttack )
            {
                round++;
            }
        }

        // Определение победителя
        FighterBase winner = fighters.FirstOrDefault( f => f.IsAlive() );
        if ( winner != null )
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine( "\n=== БИТВА ЗАВЕРШЕНА ===" );
            Console.WriteLine( $"{winner.Name} побеждает с {winner.GetCurrentHealth()} HP!" );
            Console.ResetColor();
        }

        ResetFightersState( fighters );
    }

    protected List<FighterBase> CreateRandomFighters()
    {
        Console.WriteLine( "\n=== СОЗДАНИЕ СЛУЧАЙНЫХ БОЙЦОВ ===" );

        // Запрашиваем количество бойцов
        int fighterCount = InputInt( "Введите количество бойцов (2-10): ", 2, MaxFightersAmount );

        // Списки возможных вариантов
        List<string> possibleNames =
            new List<string> { "Артем", "Гарри", "Леголас", "Гэндальф", "Константин", "Арагорн", "Бен", "Вектор", "Влад", "Миша" };

        List<Func<string, IRace, WeaponBase, IArmor, FighterBase>> possibleClasses =
            new List<Func<string, IRace, WeaponBase, IArmor, FighterBase>>
        {
            (name, race, weapon, armor) => new Knight(name, race, weapon, armor),
            (name, race, weapon, armor) => new Mage(name, race, weapon, armor),
            (name, race, weapon, armor) => new Berserker(name, race, weapon, armor)
        };

        Random random = new();
        List<FighterBase> fighters = new();

        for ( int i = 0; i < fighterCount; i++ )
        {
            string name = possibleNames[ random.Next( possibleNames.Count ) ];
            IRace race = GameItems.Races[ random.Next( GameItems.Races.Count ) ];
            WeaponBase weapon = GameItems.Weapons[ random.Next( GameItems.Weapons.Count ) ];
            IArmor armor = GameItems.Armors[ random.Next( GameItems.Armors.Count ) ];

            Func<string, IRace, WeaponBase, IArmor, FighterBase> fighterClass =
                possibleClasses[ random.Next( possibleClasses.Count ) ];

            FighterBase fighter = fighterClass( name, race, weapon, armor );
            fighters.Add( fighter );

            possibleNames.Remove( name );

            Console.WriteLine( $"Создан боец: {fighter.GetType().Name} {name} " +
                             $"(Раса: {race.GetType().Name}, " +
                             $"Оружие: {weapon.GetType().Name}, " +
                             $"Броня: {armor.GetType().Name})" );
        }

        return fighters;
    }

    protected FighterBase CreateFighter()
    {
        string name = InputString( "Введите имя бойца: " );
        IRace race = ChooseRace();
        WeaponBase weapon = ChooseWeapon();
        IArmor armor = ChooseArmor();
        return ChooseFighterClass( name, race, weapon, armor );
    }

    protected FighterBase ChooseFighterClass( string name, IRace race, WeaponBase weapon, IArmor armor )
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

    protected IRace ChooseRace()
    {
        Console.WriteLine( "\nВыберите расу:" );

        for ( int i = 0; i < GameItems.Races.Count; i++ )
        {
            IRace race = GameItems.Races[ i ];
            Console.WriteLine( $"{i + 1}. {race.GetType().Name.Replace( "Race", "" )} " +
                             $"(Урон: {race.Damage}, Здоровье: {race.Health}, Броня: {race.Armor})" );
        }

        int choice = InputInt( "> ", 1, GameItems.Races.Count );
        return GameItems.Races[ choice - 1 ];
    }

    protected WeaponBase ChooseWeapon()
    {
        Console.WriteLine( "\nВыберите оружие:" );

        for ( int i = 0; i < GameItems.Weapons.Count; i++ )
        {
            WeaponBase weapon = GameItems.Weapons[ i ];
            Console.WriteLine( $"{i + 1}. {weapon.GetType().Name} (Урон: {weapon.Damage})" );
        }

        int choice = InputInt( "> ", 1, GameItems.Weapons.Count );
        return GameItems.Weapons[ choice - 1 ];
    }

    protected IArmor ChooseArmor()
    {
        Console.WriteLine( "\nВыберите броню:" );

        for ( int i = 0; i < GameItems.Armors.Count; i++ )
        {
            IArmor armor = GameItems.Armors[ i ];
            Console.WriteLine( $"{i + 1}. {armor.GetType().Name} (Защита: {armor.Armor})" );
        }

        int choice = InputInt( "> ", 1, GameItems.Armors.Count );
        return GameItems.Armors[ choice - 1 ];
    }

    protected static void PrintMenu()
    {
        Console.WriteLine( "\n=== Меню (вводите соответствующие цифры)===" );
        Console.WriteLine( "1. Добавить бойцов" );
        Console.WriteLine( "2. Начать битву" );
        Console.WriteLine( "3. Энциклопедия" );
        Console.WriteLine( "4. Создать случайных бойцов" );
        Console.WriteLine( "5. Выход" );
        Console.WriteLine( "============" );
    }

    protected static void PrintEncyclopedia()
    {
        Console.WriteLine( "╔════════════════════════════════════╗" );
        Console.WriteLine( "║         ЭНЦИКЛОПЕДИЯ ИГРЫ          ║" );
        Console.WriteLine( "╚════════════════════════════════════╝\n" );

        Console.WriteLine( "\n\tРАСЫ" );
        foreach ( IRace race in GameItems.Races )
        {
            Console.WriteLine( $"{race.GetType().Name}" );
            Console.WriteLine( $"  Здоровье: {race.Health}" );
            Console.WriteLine( $"  Урон:     {race.Damage}" );
            Console.WriteLine( $"  Броня:    {race.Armor}" );
            Console.WriteLine();
        }

        Console.WriteLine( "\n\tОРУЖИЕ" );
        foreach ( WeaponBase weapon in GameItems.Weapons )
        {
            Console.WriteLine( $"{weapon.GetType().Name}" );
            Console.WriteLine( $"  Базовый урон:    {weapon.Damage}" );
            Console.WriteLine( $"  Крит. шанс:      {weapon.CriticalChance:P0}" );
            Console.WriteLine( $"  Множитель крита: {weapon.CriticalMultiplier}x" );
            Console.WriteLine();
        }

        Console.WriteLine( "\n\tБРОНЯ" );
        foreach ( IArmor armor in GameItems.Armors )
        {
            Console.WriteLine( $"{armor.GetType().Name}" );
            Console.WriteLine( $"  Защита: {armor.Armor}" );
            Console.WriteLine();
        }

        Console.WriteLine( "\n\tКЛАССЫ БОЙЦОВ" );
        Console.WriteLine( "=================================" );

        // Создаем временные объекты для демон
        IRace demoRace = new Human();
        WeaponBase demoWeapon = new Fists();
        IArmor demoArmor = new LeatherArmor();

        foreach ( KeyValuePair<string, Func<string, IRace, WeaponBase, IArmor, FighterBase>> fighterClass in GameItems.FighterClasses )
        {
            FighterBase demoFighter = fighterClass.Value( "Тестовый", demoRace, demoWeapon, demoArmor );

            Console.WriteLine( $"\n{fighterClass.Key.ToUpper()}" );
            Console.WriteLine( $"Тип: {demoFighter.GetType().Name}" );
            Console.WriteLine( $"Описание: {demoFighter.GetDescription()}" );
        }
    }

    protected static string InputString( string message )
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

    protected static int InputInt( string message, int min, int max )
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

    protected static void ResetFightersState( List<FighterBase> fighters )
    {
        foreach ( FighterBase fighter in fighters )
        {
            fighter.ResetState();
        }
    }
}