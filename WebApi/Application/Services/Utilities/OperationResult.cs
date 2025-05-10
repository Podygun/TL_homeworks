namespace Application.Services.Utilities;

public sealed class OperationResult
{
    public bool Success { get; set; }
    public string? ErrorMessage { get; set; }

    public OperationResult( bool success, string? errorMessage = null )
    {
        Success = success;
        ErrorMessage = errorMessage;
    }

    /// <summary>
    /// Default value of Success is True
    /// </summary>
    public OperationResult()
    {
        Success = true;
    }
}
