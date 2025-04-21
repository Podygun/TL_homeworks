
namespace AdvancedDictionary;

internal class Program
{
    static void Main( string[] args )
    {
        DictionaryManager manager = new( "dictionary.txt" );
        ConsoleHelper.PrintSuccess( "Консольный словарь (англ ↔ рус)" );

        while ( true )
        {
            ConsoleHelper.PrintNotify( "\nВыберите действие (цифра или клавиша):" );
            Console.WriteLine( "1) Перевод слов    (Enter)" );
            Console.WriteLine( "2) Добавление слов (+)" );
            Console.WriteLine( "3) Выход           (Esc)" );
            Console.WriteLine( "4) О программе     (F1)" );
            var key = Console.ReadKey( true ).Key;

            if ( key == ConsoleKey.Enter || key == ConsoleKey.D1 )
            {
                TranslateWord( manager );
            }
            else if ( key == ConsoleKey.Add || key == ConsoleKey.OemPlus || key == ConsoleKey.D2 )
            {
                AddWord( manager );
            }
            else if ( key == ConsoleKey.F1 || key == ConsoleKey.D4 )
            {
                PrintProgramInfo();
            }
            else if ( key == ConsoleKey.Escape || key == ConsoleKey.D3 )
            {
                return;
            }
            else
            {
                ConsoleHelper.PrintError( "Неверный ввод" );
            }
        }
    }

    private static void PrintProgramInfo()
    {
        Console.WriteLine();
        Console.WriteLine( """
            Консольный словарь, который работает с английским и русским языком.
            Автоматически подбирает перевод - язык на котором вы переводите не важен.
            Поддерживает несколько вариантов перевода слова (словосочетания).
            Данные сохраняются в файле dictionary.txt в папке с программой, 
            который можно посмотреть в любом блокноте.
            """ );
    }

    static void AddWord( DictionaryManager manager )
    {
        Console.Write( "\nВведите слово на английском: " );
        string? eng = ConsoleHelper.ReadString();

        Console.Write( "Введите перевод на русский: " );
        string? ru = ConsoleHelper.ReadString();

        manager.Add( eng, ru );

        ConsoleHelper.PrintSuccess( "Слово успешно добавлено!" );
    }

    static void TranslateWord( DictionaryManager manager )
    {
        Console.Write( "\nВведите текст для перевода: " );
        string? word = ConsoleHelper.ReadString();

        var translations = manager.Get( word );

        if ( translations.Count == 0 )
        {
            ConsoleHelper.PrintWarning( "Перевод не найден." );
        }
        else
        {
            ConsoleHelper.PrintSuccess( $"\nНайденные переводы слова {word}:" );
            foreach ( var translation in translations )
            {
                Console.WriteLine( $"  {translation}" );
            }
        }
    }
}
