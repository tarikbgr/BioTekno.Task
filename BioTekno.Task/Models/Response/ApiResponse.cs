namespace BioTekno.Task.Models.Response;

public enum Status { Success, Failed }
public class ApiResponse<T>
{

    public Status Status { get; set; }
    public string ResultMessage { get; set; }
    public int ErrorCode { get; set; }
    public T Data { get; set; }

    public ApiResponse(T data, Status status = Status.Success, int errorCode = 200, string resultMessage = "Your operation has been completed successfully.")
    {
        Data = data;
        Status = status;
        ErrorCode = errorCode;
        ResultMessage = resultMessage;
    }
}

