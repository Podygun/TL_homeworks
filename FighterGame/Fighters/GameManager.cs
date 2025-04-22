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
    private const int _MAX_FIGHTERS_AMOUNT = 10;

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
                    _fighters = CreateRandomFighters();
                    Fight( _fighters );
                    return;
                case 5:
                    return;
            }
        }
    }

    private List<IFighter> CreateFighters()
    {
        int amountFighters = InputInt( "Введите кол-во игроков", 2, _MAX_FIGHTERS_AMOUNT );

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

            IFighter attacker = fighters[ currentAttackerIndex ];
            Console.WriteLine( $"\n--- Раунд {round} ---\n" );
            Console.WriteLine( $"Ходит: {attacker.Name}" );

            bool idMadeAttack = false;

            // Атака всех остальных живых бойцов
            for ( int i = 0; i < fighters.Count; i++ )
            {
                if ( i == currentAttackerIndex || !fighters[ i ].IsAlive() ) continue;

                IFighter defender = fighters[ i ];

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
        IFighter winner = fighters.FirstOrDefault( f => f.IsAlive() );
        if ( winner != null )
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine( "\n=== БИТВА ЗАВЕРШЕНА ===" );
            Console.WriteLine( $"{winner.Name} побеждает с {winner.GetCurrentHealth()} HP!" );
            Console.ResetColor();
        }

        ResetFightersState( fighters );
    }

    public List<IFighter> CreateRandomFighters()
    {
        Console.WriteLine( "\n=== СОЗДАНИЕ СЛУЧАЙНЫХ БОЙЦОВ ===" );

        // Запрашиваем количество бойцов
        int fighterCount = InputInt( "Введите количество бойцов (2-10): ", 2, _MAX_FIGHTERS_AMOUNT );

        // Списки возможных вариантов
        var possibleNames = new List<string> { "Артем", "Гарри", "Леголас", "Гэндальф", "Константин", "Арагорн", "Бен", "Вектор", "Влад", "Миша" };
        var possibleClasses = new List<Func<string, IRace, WeaponBase, IArmor, IFighter>>
    {
        (name, race, weapon, armor) => new Knight(name, race, weapon, armor),
        (name, race, weapon, armor) => new Mage(name, race, weapon, armor),
        (name, race, weapon, armor) => new Berserker(name, race, weapon, armor)
    };

        Random random = new();
        List<IFighter> fighters = new();

        for ( int i = 0; i < fighterCount; i++ )
        {
            string name = possibleNames[ random.Next( possibleNames.Count ) ];
            IRace race = GameItems.Races[ random.Next( GameItems.Races.Count ) ];
            WeaponBase weapon = GameItems.Weapons[ random.Next( GameItems.Weapons.Count ) ];
            IArmor armor = GameItems.Armors[ random.Next( GameItems.Armors.Count ) ];
            var fighterClass = possibleClasses[ random.Next( possibleClasses.Count ) ];

            IFighter fighter = fighterClass( name, race, weapon, armor );
            fighters.Add( fighter );

            possibleNames.Remove( name );

            Console.WriteLine( $"Создан боец: {fighter.GetType().Name} {name} " +
                             $"(Раса: {race.GetType().Name}, " +
                             $"Оружие: {weapon.GetType().Name}, " +
                             $"Броня: {armor.GetType().Name})" );
        }

        return fighters;
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

    private static void PrintMenu()
    {
        Console.WriteLine( "\n=== Меню (вводите соответствующие цифры)===" );
        Console.WriteLine( "1. Добавить бойцов" );
        Console.WriteLine( "2. Начать битву" );
        Console.WriteLine( "3. Энциклопедия" );
        Console.WriteLine( "4. Создать случайных бойцов" );
        Console.WriteLine( "5. Выход" );
        Console.WriteLine( "============" );
    }

    private static void PrintEncyclopedia()
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
            Console.WriteLine();
        }

        Console.WriteLine( "\n\tОРУЖИЕ" );
        foreach ( var weapon in GameItems.Weapons )
        {
            Console.WriteLine( $"{weapon.GetType().Name}" );
            Console.WriteLine( $"  Базовый урон:    {weapon.Damage}" );
            Console.WriteLine( $"  Крит. шанс:      {weapon.CriticalChance:P0}" );
            Console.WriteLine( $"  Множитель крита: {weapon.CriticalMultiplier}x" );
            Console.WriteLine();
        }

        Console.WriteLine( "\n\tБРОНЯ" );
        foreach ( var armor in GameItems.Armors )
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

        foreach ( var fighterClass in GameItems.FighterClasses )
        {
            IFighter demoFighter = fighterClass.Value( "Тестовый", demoRace, demoWeapon, demoArmor );

            Console.WriteLine( $"\n{fighterClass.Key.ToUpper()}" );
            Console.WriteLine( $"Тип: {demoFighter.GetType().Name}" );
            Console.WriteLine( $"Описание: {demoFighter.GetDescription()}" );
        }
    }

    private static string InputString( string message )
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

    private static int InputInt( string message, int min, int max )
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

    private static void ResetFightersState( List<IFighter> fighters )
    {
        foreach ( var fighter in fighters )
        {
            fighter.ResetState();
        }
    }
}