using System.Net;
using System.Net.Mail;
using BE_AMPerfume.Core.Models;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;

public class EmailService : IEmailService
{
    private readonly IConfiguration _config;
    private readonly IMemoryCache _cache;

    public EmailService(IConfiguration config, IMemoryCache cache)
    {
        _config = config;
        _cache = cache;
    }

    public async Task SendVerificationCodeAsync(string toEmail, string fullName, string code)
    {
        var fromEmail = _config["EmailSettings:FromEmail"];
        var password = _config["EmailSettings:AppPassword"];

        var smtpClient = new SmtpClient("smtp.gmail.com")
        {
            Port = 587,
            Credentials = new NetworkCredential(fromEmail, password),
            EnableSsl = true,
        };

        var body = $"<h3>Xin chào {fullName},</h3><p>Mã xác thực của bạn là: <strong>{code}</strong></p>";
        var mail = new MailMessage(fromEmail, toEmail, "Xác minh tài khoản - AMPerfume", body)
        {
            IsBodyHtml = true
        };

        await smtpClient.SendMailAsync(mail);
    }

    public Task SaveOtpAsync(string email, string code)
    {
        var cacheKey = $"otp:{email.ToLower()}";
        _cache.Set(cacheKey, code, TimeSpan.FromMinutes(5));
        return Task.CompletedTask;
    }

    public Task<bool> VerifyOtpAsync(string email, string code)
    {
        var cacheKey = $"otp:{email.ToLower()}";
        if (_cache.TryGetValue(cacheKey, out string savedCode))
        {
            return Task.FromResult(savedCode == code);
        }
        return Task.FromResult(false);
    }

    public Task SendVerificationCodeAsync(string email, object value, string otp)
    {
        if (value is string fullName)
        {
            return SendVerificationCodeAsync(email, fullName, otp);
        }
        else if (value is User user)
        {
            return SendVerificationCodeAsync(email, user.Name ?? email, otp);
        }
        else
        {
            throw new ArgumentException("Invalid value type for sending verification code.");
        }
    }
}
