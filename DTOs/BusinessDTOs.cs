namespace BusinessManagement.DTOs
{
    // ─── Request DTOs ───────────────────────────────────────────────────────────

    public class AddBusinessRequest
    {
        public string BusinessName { get; set; } = string.Empty;
        public string CreatorId { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;
    }

    public class EditBusinessRequest
    {
        public string BusinessName { get; set; } = string.Empty;
        public string CreatorId { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;
    }

    // ─── Response DTOs ──────────────────────────────────────────────────────────

    public class BusinessResponse
    {
        public string BusinessId { get; set; } = string.Empty;
        public string BusinessName { get; set; } = string.Empty;
        public string CreatorId { get; set; } = string.Empty;
        public string CreatorName { get; set; } = string.Empty;
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public bool IsDeleted { get; set; }
    }

    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public T? Data { get; set; }

        public static ApiResponse<T> Ok(T data, string message = "Success") =>
            new() { Success = true, Message = message, Data = data };

        public static ApiResponse<T> Fail(string message) =>
            new() { Success = false, Message = message, Data = default };
    }
}
