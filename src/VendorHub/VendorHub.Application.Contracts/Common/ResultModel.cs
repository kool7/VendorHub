namespace VendorHub.Application.Contracts.Common;

public class ResultModel<T>
{
    public bool IsSuccess { get; set; }
    public T Data { get; set; }
    public List<string> HasErrors { get; set; }

    private ResultModel(bool success, T data)
    {
        IsSuccess = success;
        Data = data;
    }

    private ResultModel(bool success, T data, List<string> error)
    {
        IsSuccess = success;
        Data = data;
        HasErrors = error;
    }

    public static ResultModel<T> Success(T data)
    {
        return new ResultModel<T>(true, data);
    }

    public static ResultModel<T> Error(List<string> errors)
    {
        return new ResultModel<T>(false, default(T), errors);
    }
}
