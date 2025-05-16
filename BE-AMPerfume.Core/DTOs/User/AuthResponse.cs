public class AuthResponseDTO
{
    public string Token { get; set; }
    public bool IsSuccess { get; set; }
    public string Message { get; set; }  = string.Empty;
    public string FullName { get; set; }
    public string Email { get; set; }
}