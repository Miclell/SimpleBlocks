namespace SimpleBlocks.Shared.Models;

public class ApiResponse<T>
{
    public bool IsSuccessful { get; set; }
    public T? ResponseData { get; set; }
    public string? ErrorMessage { get; set; }

    public static ApiResponse<T> Success(T data) => new() { IsSuccessful = true, ResponseData = data };
    public static ApiResponse<object> Error(string? message) => new() { IsSuccessful = false, ErrorMessage = message };
} 