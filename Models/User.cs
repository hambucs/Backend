using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
namespace Szakdoga.Backend.Models
{
    public class User
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Hiányzik a felhasználónév.")]
        public string Username { get; set; } // Required for user login

        [Required(ErrorMessage = "Hiányzik az email.")]
        public string Email { get; set; } // Required for user login

        [Required(ErrorMessage = "Hiányzik a jelszó.")]
        public string PasswordHash { get; set; } // Store hashed password

        public string Role { get; set; } = "User"; // Default to "User" if no specific role is needed

        // Navigation properties
      
        public List<Car>? Cars { get; set; }
             
        public List<Rental>? Rentals { get; set; }
    }
}