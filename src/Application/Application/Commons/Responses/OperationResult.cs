namespace Application.Commons.Responses;

public struct OperationResult
{
    public bool IsSuccess { get; set; }
    public string[] Messages { get; set; }

    private OperationResult(bool isSuccess, string[] messages)
    {
        IsSuccess = isSuccess;
        Messages = messages;
    }
    public static OperationResult Success() => new OperationResult(true, Array.Empty<string>());
    public static OperationResult Failure(params string[] messages) => new OperationResult(false, messages);
}
