using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Szakdoga.Backend.Data;
using Szakdoga.Backend.Models;
using Szakdoga.Backend.Services;

namespace Szakdoga.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly CarRentalContext _context;
        private readonly TokenService _tokenService;
        private readonly PasswordHasher _passwordHasher; // Added PasswordHasher service

        public AuthController(CarRentalContext context, TokenService tokenService, PasswordHasher passwordHasher)
        {
            _context = context;
            _tokenService = tokenService;
            _passwordHasher = passwordHasher; // Inject PasswordHasher
        }

        [HttpPost("login")]
        public async Task<ActionResult<TokenResponse>> Login([FromBody] LoginRequest request)
        {
            if (request == null)
            {
                return BadRequest("Hibás kérés.");
            }

            // Fetch the user from the database
            var user = await _context.Users
                .SingleOrDefaultAsync(u => u.Email == request.Email);

            // Check if user is null or if password doesn't match
            if (user == null || !_passwordHasher.VerifyPassword(request.Password, user.PasswordHash))
            {
                return Unauthorized("Hibás értékek.");
            }

            // Generate the token using the User object
            var token = _tokenService.GenerateToken(user); // Pass the User object here
            return Ok(new TokenResponse { Token = token }); // Return TokenResponse object
        }
    }

    // New PasswordHasher service
    public class PasswordHasher
    {
        public string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var bytes = System.Text.Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash);
            }
        }

        public bool VerifyPassword(string password, string storedHash)
        {
            return HashPassword(password) == storedHash;
        }
    }

    // Token response object
    public class TokenResponse
    {
        public string Token { get; set; }
    }
}