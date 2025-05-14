using Fighters;

namespace Figters.Tests.Sevices.GameManagerTests;

public sealed class InputTests : IDisposable
{

    private StringWriter _stringWriter;
    private StringReader _stringReader;

    public InputTests()
    {
        _stringWriter = new StringWriter();
        Console.SetOut( _stringWriter );
    }

    public void Dispose()
    {
        _stringWriter.Dispose();
        Console.SetOut( Console.Out );
    }


    [Fact]
    public void InputString_ShouldReturnValidInput_WhenInputIsValid()
    {
        // Arrange
        string input = "Test Input";
        _stringReader = new StringReader( input );
        Console.SetIn( _stringReader );

        // Act
        string result = MockGameManager.InputString( "Введите строку: " );

        // Assert
        Assert.Equal( input, result );
    }


    [Fact]
    public void InputString_ShouldPromptForInput_WhenInputIsEmpty()
    {
        // Arrange
        string input = "\nTest Input"; // Пустая строка, затем валидный ввод
        _stringReader = new StringReader( input );
        Console.SetIn( _stringReader );

        // Act
        string result = MockGameManager.InputString( "Введите строку: " );

        // Assert
        Assert.Equal( "Test Input", result );
        Assert.DoesNotContain( "Test Input", _stringWriter.ToString() );
    }


    [Fact]
    public void InputInt_ShouldReturnValidInput_WhenInputIsValid()
    {
        // Arrange
        string input = "5"; // Вводим валидное число
        _stringReader = new StringReader( input );
        Console.SetIn( _stringReader );

        // Act
        int result = MockGameManager.InputInt( "Введите число от 1 до 10: ", 1, 10 );

        // Assert
        Assert.Equal( 5, result );
    }


    [Fact]
    public void InputInt_ShouldPromptForInput_WhenInputIsOutOfRange()
    {
        // Arrange
        string input = "15\n7"; //15 - вне диапазона, 7 - в диапазоне
        _stringReader = new StringReader( input );
        Console.SetIn( _stringReader );

        // Act
        int result = MockGameManager.InputInt( "Введите число от 1 до 10: ", 1, 10 );

        // Assert
        Assert.Equal( 7, result );
        Assert.DoesNotContain( "7", _stringWriter.ToString() );
    }


    [Fact]
    public void InputInt_ShouldPromptForInput_WhenInputIsNotANumber()
    {
        // Arrange
        string input = "abc\n5";
        _stringReader = new StringReader( input );
        Console.SetIn( _stringReader );

        // Act
        int result = MockGameManager.InputInt( "Введите число от 1 до 10: ", 1, 10 );

        // Assert
        Assert.Equal( 5, result );
        Assert.DoesNotContain( "5", _stringWriter.ToString() );
    }
}
