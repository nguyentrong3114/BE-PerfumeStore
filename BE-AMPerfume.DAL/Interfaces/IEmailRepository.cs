public interface IEmailRepository
{
    Task SendVerificationCodeAsync(string toEmail, string fullName, string code);
}