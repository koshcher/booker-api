namespace BookerApi.Lib;

public class Error
{
    public string Message { get; set; }

    public Error(string message)
    {
        Message = message;
    }

    public static implicit operator Error(string message) => new(message);
}

public class DataResult<T>
{
    public T Data { get; }

    public DataResult(T data)
    {
        Data = data;
    }
}

public class ErrorResult
{
    public Error Error { get; set; }

    public ErrorResult(Error error)
    {
        Error = error;
    }
}

public class EmptyResult
{ }