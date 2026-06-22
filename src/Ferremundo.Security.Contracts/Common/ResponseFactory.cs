namespace Ferremundo.Security.Contracts.Common;

public static class ResponseFactory
{
    public static ResponseBase<T> Success<T>(
        T data,
        string message = "Request completed successfully.",
        string code = ResponseCodes.Success)
    {
        return new ResponseBase<T>
        {
            Success = true,
            Code = code,
            Message = message,
            Data = data
        };
    }

    public static ResponseBase<T> Fail<T>(
        string code,
        string message)
    {
        return new ResponseBase<T>
        {
            Success = false,
            Code = code,
            Message = message,
            Data = default
        };
    }
}
