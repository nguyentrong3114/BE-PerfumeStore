using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

public class OtpRepository : IOtpRepository
{
    // In-memory store for demo. Replace with DB in production.
    private static ConcurrentDictionary<string, (string Otp, DateTime ExpiredAt)> _store = new();

    public Task SaveOtpAsync(string email, string otp, DateTime expiredAt)
    {
        _store[email] = (otp, expiredAt);
        return Task.CompletedTask;
    }

    public Task<bool> VerifyOtpAsync(string email, string otp)
    {
        if (_store.TryGetValue(email, out var entry))
        {
            if (entry.Otp == otp && entry.ExpiredAt > DateTime.UtcNow)
                return Task.FromResult(true);
        }
        return Task.FromResult(false);
    }

    public Task RemoveOtpAsync(string email)
    {
        _store.TryRemove(email, out _);
        return Task.CompletedTask;
    }
}