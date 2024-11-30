using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Szakdoga.Backend.Data;
using Szakdoga.Backend.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Szakdoga.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly CarRentalContext _context;

        public CarsController(CarRentalContext context)
        {
            _context = context;
        }

        // Get all available cars based on filters
        [HttpGet]
        public async Task<ActionResult<List<Car>>> GetAvailableCars(string? location = null, DateTime? startDate = null, DateTime? endDate = null)
        {
            var query = _context.Cars.AsQueryable();

            // Filter by location
            if (!string.IsNullOrEmpty(location))
            {
                query = query.Where(car => car.Location.Contains(location));
            }

            // Filter by availability dates
            if (startDate.HasValue && endDate.HasValue)
            {
                query = query.Where(car => car.AvailableFrom <= startDate && car.AvailableTo >= endDate && car.IsAvailable);
            }

            var cars = await query.Include(c => c.Owner).ToListAsync();

            // Return a simplified list of cars (flatten the data)
            var simplifiedCars = cars.Select(car => new
            {
                car.ID,
                car.OwnerID,
                car.Make,
                car.Model,
                car.Year,
                car.DailyRate,
                car.AvailableFrom,
                car.AvailableTo,
                car.Description,
                car.Location,
                car.Mileage,
                car.IsAvailable,
                car.Image,
                Owner = new
                {
                    car.Owner.ID,
                    car.Owner.Username,
                    car.Owner.Email
                }
            }).ToList();

            return Ok(simplifiedCars);
        }

        // Get a specific car by ID
        [HttpGet("{id}")]
        public async Task<ActionResult<Car>> GetCar(int id)
        {
            var car = await _context.Cars.Include(c => c.Owner).FirstOrDefaultAsync(c => c.ID == id);
            if (car == null) return NotFound();

            return car;
        }

        // Update an existing car (only the owner can update)
        [HttpPut("{id}")]
        [Authorize]
        public async Task<IActionResult> UpdateCar(int id, [FromForm] Car car, [FromForm] IFormFile? imageFile)
        {
            if (id != car.ID) return BadRequest();

            // Get the logged-in user's ID
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));

            // Check if the car belongs to the logged-in user
            var existingCar = await _context.Cars.FindAsync(id);
            if (existingCar == null || existingCar.OwnerID != userId) return Forbid();

            // Update car details
            existingCar.Make = car.Make;
            existingCar.Model = car.Model;
            existingCar.Year = car.Year;
            existingCar.DailyRate = car.DailyRate;
            existingCar.Description = car.Description;
            existingCar.Location = car.Location;
            existingCar.AvailableFrom = car.AvailableFrom;
            existingCar.AvailableTo = car.AvailableTo;

            // Handle file upload for updated image
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    existingCar.Image = memoryStream.ToArray(); // Save image as binary data
                }
            }

            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Delete a car (only the owner can delete)
        [HttpDelete("{id}")]
        [Authorize]
        public async Task<IActionResult> DeleteCar(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            var car = await _context.Cars.FindAsync(id);

            if (car == null || car.OwnerID != userId) return Forbid();

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Create a new car
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreateCar([FromForm] Car car, [FromForm] IFormFile? imageFile)
        {
            // Validate model state
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    errors = ModelState.Values
                        .SelectMany(v => v.Errors)
                        .Select(e => e.ErrorMessage)
                });
            }

            // Get the logged-in user's ID
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier));
            car.OwnerID = userId;
            car.IsAvailable = true;

            // Handle file upload
            if (imageFile != null && imageFile.Length > 0)
            {
                using (var memoryStream = new MemoryStream())
                {
                    await imageFile.CopyToAsync(memoryStream);
                    car.Image = memoryStream.ToArray(); // Save image as binary data
                }
            }

            // Save car to database
            _context.Cars.Add(car);
            await _context.SaveChangesAsync();

            // Return the created car details with its generated ID
            return CreatedAtAction(nameof(GetCar), new { id = car.ID }, car);
        }
        [HttpGet("api/cars/{carId}/availability")]
        public IActionResult GetCarAvailability(int carId)
        {
            var car = _context.Cars.FirstOrDefault(c => c.ID == carId);

            if (car == null)
                return NotFound();

            var rentals = _context.Rentals
                .Where(r => r.CarID == carId)
                .Select(r => new { r.RentalStart, r.RentalEnd })
                .ToList();

            return Ok(new
            {
                availableFrom = car.AvailableFrom,
                availableTo = car.AvailableTo,
                rentals = rentals
            });
        }

        // Check if a car exists
        private bool CarExists(int id) => _context.Cars.Any(e => e.ID == id);
    }
}