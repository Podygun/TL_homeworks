using Accommodations.Commands;
using Accommodations.Dto;

namespace Accommodations;

public static class AccommodationsProcessor
{
    // Добавлена константа для максимального кол-ва дней для брони
    private const int BookMaxDays = 30;
    private static BookingService _bookingService = new();
    private static Dictionary<int, ICommand> _executedCommands = new();
    private static int s_commandIndex = 0;

    public static void Run()
    {
        Console.WriteLine( "Booking Command Line Interface" );
        Console.WriteLine( "Commands:" );
        Console.WriteLine( "'book <UserId> <Category> <StartDate> <EndDate> <Currency>' - to book a room" );
        Console.WriteLine( "'cancel <BookingId>' - to cancel a booking" );
        Console.WriteLine( "'undo' - to undo the last command" );
        Console.WriteLine( "'find <BookingId>' - to find a booking by ID" );
        Console.WriteLine( "'search <StartDate> <EndDate> <CategoryName>' - to search bookings" );
        Console.WriteLine( "'exit' - to exit the application" );

        string input;
        while ( ( input = Console.ReadLine() ) != "exit" )
        {
            try
            {
                ProcessCommand( input );
            }
            catch ( ArgumentException ex )
            {
                Console.WriteLine( $"Error: {ex.Message}" );
            }
        }
    }

    private static void ProcessCommand( string input )
    {
        string[] parts = input.Split( ' ' );
        string commandName = parts[ 0 ];

        switch ( commandName )
        {
            case "book":
                HandleBook( parts );
                break;

            case "cancel":
                HandleCancel( parts );
                break;

            case "undo":
                HandleUndo( parts );
                break;

            case "find":
                HandleFind( parts );
                break;

            case "search":
                HandleSearch( parts );
                break;

            default:
                Console.WriteLine( "Unknown command." );
                break;
        }
    }

    private static void HandleBook( string[] parts )
    {
        if ( parts.Length != 6 )
        {
            Console.WriteLine( "Invalid number of arguments for booking." );
            return;
        }

        // Добавлена валидация id пользователя
        if ( !int.TryParse( parts[ 1 ], out int userId ) )
        {
            Console.WriteLine( "Invalid user id." );
            return;
        }

        // Добавлена проверка на валидность введенных пользователем дат
        var StartDateTimeParsed = ParseDateTime( parts[ 3 ], "Invalid start date for booking." );
        var EndDateTimeParsed = ParseDateTime( parts[ 4 ], "Invalid end date for booking." );

        // Добавлена проверка оформления брони на слишком долгое время
        TimeSpan difference = EndDateTimeParsed - StartDateTimeParsed;
        int daysDifference = difference.Days;

        if ( daysDifference > BOOK_MAX_DAYS )
        {
            Console.WriteLine( $"Error: Booking duration limit reached. Your selected period exceeds our maximum of {BOOK_MAX_DAYS} days." );
            return;
        }

        // Изменен парсинг валюты, сделан вывод валидной валюты для использования
        if ( !Enum.TryParse( parts[ 5 ], true, out CurrencyDto currency ) )
        {
            string validCurrency = string.Join( ", ", Enum.GetNames( typeof( CurrencyDto ) ) );
            Console.WriteLine( $"Invalid currency: '{parts[ 5 ]}'. Use available values: {validCurrency.ToLower()}" );
            return;
        }

        BookingDto bookingDto = new()
        {
            UserId = userId,
            Category = parts[ 2 ],
            StartDate = StartDateTimeParsed,
            EndDate = EndDateTimeParsed,
            Currency = currency,
        };

        BookCommand bookCommand = new( _bookingService, bookingDto );
        bookCommand.Execute();
        _executedCommands.Add( ++s_commandIndex, bookCommand );
        Console.WriteLine( "Booking command run is successful." );
    }

    private static void HandleCancel( string[] parts )
    {
        if ( parts.Length != 2 )
        {
            Console.WriteLine( "Invalid number of arguments for canceling." );
            return;
        }

        // Изменен парсинг Guid через TryParse
        if ( !Guid.TryParse( parts[ 1 ], out Guid bookingId ) )
        {
            Console.WriteLine( "Invalid id for booking." );
            return;
        }

        CancelBookingCommand cancelCommand = new( _bookingService, bookingId );
        cancelCommand.Execute();
        _executedCommands.Add( ++s_commandIndex, cancelCommand );
        Console.WriteLine( "Cancellation command run is successful." );
    }

    private static void HandleUndo( string[] parts )
    {
        // Добавлена проверка индекса команды при отмене
        if ( s_commandIndex < 1 )
        {
            Console.WriteLine( "There's no nothing to undo" );
            return;
        }
        _executedCommands[ s_commandIndex ].Undo();
        _executedCommands.Remove( s_commandIndex );
        s_commandIndex--;
        Console.WriteLine( "Last command undone." );
    }

    private static void HandleFind( string[] parts )
    {
        if ( parts.Length != 2 )
        {
            Console.WriteLine( "Invalid arguments for 'find'. Expected format: 'find <BookingId>'" );
            return;
        }

        // Изменен парсинг Guid через TryParse
        if ( !Guid.TryParse( parts[ 1 ], out Guid id ) )
        {
            Console.WriteLine( "Invalid id for booking." );
            return;
        }
        FindBookingByIdCommand findCommand = new( _bookingService, id );
        findCommand.Execute();

        // Добавлено добавление операции в выполненные команды
        _executedCommands.Add( ++s_commandIndex, findCommand );
    }

    private static void HandleSearch( string[] parts )
    {
        if ( parts.Length != 4 )
        {
            Console.WriteLine( "Invalid arguments for 'search'. Expected format: 'search <StartDate> <EndDate> <CategoryName>'" );
            return;
        }

        // Добавлена проверка на валидность введенных пользователем дат
        var startDate = ParseDateTime( parts[ 1 ], "Invalid start date for searching." );
        var endDate = ParseDateTime( parts[ 2 ], "Invalid start date for searching." );

        string categoryName = parts[ 3 ];
        SearchBookingsCommand searchCommand = new( _bookingService, startDate, endDate, categoryName );
        searchCommand.Execute();

        // Добавлено добавление операции в выполненные команды
        _executedCommands.Add( ++s_commandIndex, searchCommand );
    }

    private static DateTime ParseDateTime( string value, string errorMessage )
    {
        if ( !DateTime.TryParse( value, out DateTime dateTimeParsed ) )
        {
            throw new InvalidDataException( errorMessage );
        }
        return dateTimeParsed;
    }
}
