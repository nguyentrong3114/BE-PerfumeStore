public interface IEmailService
{
    Task SendVerificationCodeAsync(string toEmail, string fullName, string code);
    Task SaveOtpAsync(string email, string code);
    Task<bool> VerifyOtpAsync(string email, string code);
}
