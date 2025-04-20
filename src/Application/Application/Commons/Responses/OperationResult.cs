namespace Application.Commons.Responses;

public struct OperationResult
{
    public bool IsSuccess { get; set; }
    public string[] Messages { get; set; }
    public object Data { get; set; }
    private OperationResult(bool isSuccess, string[] messages)
    {
        IsSuccess = isSuccess;
        Messages = messages;
    }

    private OperationResult(bool isSuccess, string[] messages, object data)
    {
        IsSuccess = isSuccess;
        Messages = messages;
        Data = data;
    }
    public static OperationResult Success() => new OperationResult(true, Array.Empty<string>());
    public static OperationResult Success(object data) => new OperationResult(true, Array.Empty<string>(), data);
    public static OperationResult Failure(params string[] messages) => new OperationResult(false, messages);
}
