public interface IOtpRepository
{
    Task SaveOtpAsync(string email, string otp, DateTime expiredAt);
    Task<bool> VerifyOtpAsync(string email, string otp);
    Task RemoveOtpAsync(string email);
}