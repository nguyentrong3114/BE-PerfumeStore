public interface IForgotPasswordService
{
    Task<bool> SendOtpToEmailAsync(string email);
    Task<bool> VerifyOtpAsync(string email, string otp);
    Task<bool> ResetPasswordAsync(string email, string newPassword, string otp);
}