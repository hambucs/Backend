using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Szakdoga.Backend.Models
{
    public class Car
    {
        public int ID { get; set; }

        [Required(ErrorMessage = "Hiányzik a tulajdonos ID-je.")]
        public int OwnerID { get; set; } // Foreign key to User

        [Required(ErrorMessage = "Hiányzik az autó márkaneve.")]
        [StringLength(50, ErrorMessage = "A márkanév nem lehet 50 karakternél hosszabb.")]
        public string Make { get; set; } // Make of the car

        [Required(ErrorMessage = "Hiányzik az autó modellneve.")]
        [StringLength(50, ErrorMessage = "A modellnév nem lehet hosszabb 50 karakternél.")]
        public string Model { get; set; } // Model of the car

        [Range(1886, 2024, ErrorMessage = "Hibás dátum.")]
        public int Year { get; set; } // Year of manufacture

        [Range(0, 1000000, ErrorMessage = "A napidíjnak pozitív értéknek kell lennie.")]
        public decimal DailyRate { get; set; } // Rental cost per day

        [Required(ErrorMessage = "Hiányzik az elérhetőség kezdete.")]
        public DateTime AvailableFrom { get; set; } // Car availability start date

        [Required(ErrorMessage = "Hiányzik az elérhetőség vége.")]
        public DateTime AvailableTo { get; set; } // Car availability end date

        [StringLength(500, ErrorMessage = "A leírás nem lehet hosszabb 500 karakternél.")]
        public string? Description { get; set; } // Optional description of the car

        [StringLength(100, ErrorMessage = "A helyszín nem lehet hosszabb 100 karakternél.")]
        public string? Location { get; set; } // Location of the car for pickup

        [Range(0, 1000000, ErrorMessage = "A futásteljesítménynek pozitív értéknek kell lennie.")]
        public int Mileage { get; set; } // Mileage of the car

        public bool IsAvailable { get; set; } = true; // Availability status of the car

       public byte[]? Image { get; set; }

        // Navigation properties
        public User? Owner { get; set; } // Reference to the owner (User model)
        public List<Rental>? Rentals { get; set; } // List of rentals associated with the car
    }
}