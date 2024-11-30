using Microsoft.AspNetCore.Identity;
using Szakdoga.Backend.Models;

namespace Szakdoga.Backend.Services
{
    public class PasswordHasherService
    {
        private readonly PasswordHasher<User> _passwordHasher;

        public PasswordHasherService()
        {
            _passwordHasher = new PasswordHasher<User>();
        }

        // Method to hash a password
        public string HashPassword(User user, string password)
        {
            return _passwordHasher.HashPassword(user, password);
        }

        // Method to verify a password
        public bool VerifyPassword(User user, string password, string storedHash)
        {
            var result = _passwordHasher.VerifyHashedPassword(user, storedHash, password);
            return result == PasswordVerificationResult.Success;
        }
    }
}