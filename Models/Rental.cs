using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Szakdoga.Backend.Models
{
    public class Rental
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Hiányzik az autó ID-je.")]
        public int CarID { get; set; } // Foreign key to Car

        [Required(ErrorMessage = "Hiányzik a kölcsönző ID-je.")]
        public int RenterID { get; set; } // Foreign key to User

        [Required(ErrorMessage = "Hiányzik a kölcsönzés elejének a dátuma.")]
        public DateTime RentalStart { get; set; }

        [Required(ErrorMessage = "Hiányzik a kölcsönzés végének a dátuma.")]
        public DateTime RentalEnd { get; set; }

       

        // Navigation properties
        public Car? Car { get; set; }
        public User? Renter { get; set; }
    }
}