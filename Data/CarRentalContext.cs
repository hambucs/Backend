using Microsoft.EntityFrameworkCore;
using Szakdoga.Backend.Models;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Szakdoga.Backend.Data
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options)
            : base(options)
        {
        }

        // DbSet properties for the entities
        public DbSet<Car> Cars { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Rental> Rentals { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships
            modelBuilder.Entity<Car>()
                .HasOne(c => c.Owner)
                .WithMany(u => u.Cars)
                .HasForeignKey(c => c.OwnerID);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Renter)
                .WithMany(u => u.Rentals)
                .HasForeignKey(r => r.RenterID);

            modelBuilder.Entity<Rental>()
                .HasOne(r => r.Car)
                .WithMany(c => c.Rentals)
                .HasForeignKey(r => r.CarID);

            // Seed initial data for User
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    ID = 1,
                    Username = "admin",
                    Email="admin@admin.com",
                    PasswordHash = HashPassword("securePassword123") // Hash the password securely
                }
            );

            // Seed initial data for Car
            modelBuilder.Entity<Car>().HasData(
                new Car
                {
                    ID = 1,
                    OwnerID = 1, // Admin is the owner
                    Make = "Toyota",
                    Model = "Corolla",
                    Year = 2020,
                    DailyRate = 50,
                    AvailableFrom = DateTime.Now.AddDays(1), // Available from tomorrow
                    AvailableTo = DateTime.Now.AddMonths(1), // Available until next month
                    Description = "Egy megbízható családi autó, jól karbantartva.",
                    Location = "Budapest",
                    Mileage = 25000,
                    IsAvailable = true
                }
            );

            // Seed initial data for Rental
            modelBuilder.Entity<Rental>().HasData(
                new Rental
                {
                    ID = 1,
                    CarID = 1,
                    RenterID = 1, // Assuming the admin is renting the car
                    RentalStart = DateTime.Now,
                    RentalEnd = DateTime.Now.AddDays(2) // Example rental duration
                }
            );
        }

        private string HashPassword(string password)
        {
            // Using SHA256 for password hashing
            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(password);
                var hash = sha256.ComputeHash(bytes);
                return Convert.ToBase64String(hash); // Return the base64 string of the hash
            }
        }
    }
}