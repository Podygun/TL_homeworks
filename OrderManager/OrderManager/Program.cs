namespace OrderManager;

public sealed class Program
{
    public static void Main( string[] args )
    {
        Print( "Программа Order Manager" );
        Print( "by Подыганов Владислав aka Podych" );

        while ( true )
        {
            Print( "\nДля добавления заказа введите Y" );
            Print( "Для выхода введите N" );

            string action = ReadString();

            if ( action.Equals( "N" ) )
            {
                break;
            }

            if ( action.Equals( "Y" ) )
            {
                PlaceOrder();
            }
        }
    }

    public static void PlaceOrder()
    {
        Print( "Введите название товара: ", false );
        string productName = ReadString();

        Print( "Введите кол-во товара: ", false );
        int productCount = ReadInt( true );

        Print( "Введите Ваше имя: ", false );
        string userName = ReadString();

        Print( "Введите адрес доставки: ", false );
        string userAddress = ReadString();

        Print( $"Здравствуйте, {userName}, Вы заказали {productCount} шт. {productName} на адрес {userAddress}" );
        Print( "Для подтверждения Y, для отказа N" );

        string confirm = String.Empty;

        while ( confirm.Equals( "Y" ) && confirm.Equals( "N" ) )
        {
            confirm = ReadString();
            if ( confirm.Equals( "Y" ) )
            {
                Print( $"{userName}! Ваш заказ {productName} в количестве {productCount} оформлен! " +
                    $"Ожидайте доставку по адресу {userAddress} к {DateTime.Today.AddDays( 3 ):D}" );
            }
            else if ( confirm.Equals( "N" ) )
            {
                Print( "Отмена оформления, возврат назад" );
            }
            else
            {
                Print( "Для подтверждения Y, для отказа N" );
            }
        }
    }

    public static void Print( string str, bool isNewLine = true )
    {
        if ( isNewLine )
        {
            Console.WriteLine( str );
        }
        else
        {
            Console.Write( str );
        }
    }

    public static string ReadString()
    {
        string? inputString = Console.ReadLine();
        while ( String.IsNullOrWhiteSpace( inputString ) )
        {
            Console.Write( "Пустое значение запрещено, повторите ввод: " );
            inputString = Console.ReadLine();
        }
        return inputString;
    }

    public static int ReadInt( bool isUnsigned = false )
    {
        bool isInt = false;
        int result = 0;

        if ( isUnsigned )
        {
            while ( !isInt || result <= 0 )
            {
                isInt = int.TryParse( Console.ReadLine(), out result );
                if ( !isInt || result <= 0 )
                {
                    Console.Write( "Только целое положительное число, повторите ввод: " );
                }
            }
        }
        else
        {
            while ( !isInt )
            {
                isInt = int.TryParse( Console.ReadLine(), out result );
                if ( !isInt )
                {
                    Console.Write( "Только целое число, повторите ввод: " );
                }
            }
        }
        return result;
    }
}