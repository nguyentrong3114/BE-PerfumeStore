using System;
using System.Text.Json.Serialization;

namespace BE_AMPerfume.Core.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Email { get; set; } = null!;

        [JsonIgnore]
        public string PasswordHash { get; set; } = null!;

        public string? Phone { get; set; }

        public string? Address { get; set; }

        public string? Role { get; set; }
        public virtual Cart Cart { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
