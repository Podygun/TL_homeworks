namespace AdvancedDictionary;

public class DictionaryManager
{
    private readonly string _filePath;
    private const string _separator = ":";

    public DictionaryManager( string filePath )
    {
        this._filePath = filePath;
        if ( !File.Exists( this._filePath ) )
        {
            File.Create( this._filePath ).Close();
        }
    }

    /// <summary>
    /// Получение всех переводов слова списком
    /// </summary>
    /// <param name="word">Слово для перевода</param>
    /// <returns>Список переводов слов</returns>
    public List<string> Get( string word )
    {
        List<string> translations = new List<string>();
        string[] lines = File.ReadAllLines( _filePath );

        foreach ( string line in lines )
        {
            string[] parts = line.Split( _separator );
            if ( parts.Length != 2 ) continue;  // Пропуск невалидной строки

            if ( parts[ 0 ].Equals( word, StringComparison.OrdinalIgnoreCase ) )    // Игнорирование верхнего/нижнего регистра при сравнении
            {
                translations.Add( parts[ 1 ] );
            }
            else if ( parts[ 1 ].Equals( word, StringComparison.OrdinalIgnoreCase ) )
            {
                translations.Add( parts[ 0 ] );
            }
        }
        return translations;
    }

    /// <summary>
    /// Добавить слово и перевод
    /// </summary>
    /// <param name="eng">Слово на английском языке</param>
    /// <param name="ru">Слово на русском языке</param>
    public void Add( string eng, string ru )
    {
        File.AppendAllText( _filePath, $"{eng.ToLower()}:{ru.ToLower()}\n" );
    }

}
