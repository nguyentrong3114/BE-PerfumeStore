using BE_AMPerfume.BLL.Interfaces;
using BE_AMPerfume.Core.DTOs;
using BE_AMPerfume.DAL.Interfaces;
using System;
using System.Threading.Tasks;

public class ForgotPasswordService : IForgotPasswordService
{
    private readonly IUserRepository _userRepository;
    private readonly IEmailService _emailService;

    public ForgotPasswordService(IUserRepository userRepository, IEmailService emailService)
    {
        _userRepository = userRepository;
        _emailService = emailService;
    }

    public async Task<bool> SendOtpToEmailAsync(string email)
    {
        var user = await _userRepository.GetByEmailAsync(email);
        if (user == null) return false;

        var otp = new Random().Next(100000, 999999).ToString();
        await _emailService.SaveOtpAsync(email, otp);
        await _emailService.SendVerificationCodeAsync(email, user.Name ?? email, otp);
        return true;
    }

    public async Task<bool> VerifyOtpAsync(string email, string otp)
    {
        return await _emailService.VerifyOtpAsync(email, otp);
    }

    public async Task<bool> ResetPasswordAsync(string email, string newPassword, string otp)
    {
        var valid = await _emailService.VerifyOtpAsync(email, otp);
        if (!valid) return false;

        await _userRepository.UpdatePasswordByOtpAsync(email,newPassword);
        return true;
    }
}