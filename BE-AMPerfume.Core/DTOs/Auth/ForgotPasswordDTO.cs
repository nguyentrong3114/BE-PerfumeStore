public class ForgotPasswordDTO
{
    public string Email { get; set; }
}

// filepath: c:\Users\maith\Desktop\BE-AMPerfume\BE-AMPerfume.Core\DTOs\Auth\VerifyOtpDTO.cs
public class VerifyOtpDTO
{
    public string Email { get; set; }
    public string Otp { get; set; }
}

// filepath: c:\Users\maith\Desktop\BE-AMPerfume\BE-AMPerfume.Core\DTOs\Auth\ResetPasswordDTO.cs
public class ResetPasswordDTO
{
    public string Email { get; set; }
    public string NewPassword { get; set; }
    public string Otp { get; set; }
}