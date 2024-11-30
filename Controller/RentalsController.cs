using Microsoft.AspNetCore.Authorization; // Add this for authorization
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Szakdoga.Backend.Data;
using Szakdoga.Backend.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Szakdoga.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RentalsController : ControllerBase
    {
        private readonly CarRentalContext _context;

        public RentalsController(CarRentalContext context)
        {
            _context = context;
        }

        // GET: api/rentals
        [HttpGet]
        public async Task<ActionResult<List<Rental>>> GetRentals()
        {
            return await _context.Rentals.ToListAsync();
        }

        // GET: api/rentals/{id}
        [HttpGet("{id}")]
        public async Task<ActionResult<Rental>> GetRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }
            return rental;
        }

        
        [HttpPost]
        public async Task<ActionResult<Rental>> CreateRental(Rental rental)
        {
            _context.Rentals.Add(rental);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetRental), new { id = rental.ID }, rental);
        }

       
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")] // Only admins can update rentals
        public async Task<IActionResult> UpdateRental(int id, Rental rental)
        {
            if (id != rental.ID)
            {
                return BadRequest();
            }

            _context.Entry(rental).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!RentalExists(id))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/rentals/{id}
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")] // Only admins can delete rentals
        public async Task<IActionResult> DeleteRental(int id)
        {
            var rental = await _context.Rentals.FindAsync(id);
            if (rental == null)
            {
                return NotFound();
            }

            _context.Rentals.Remove(rental);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool RentalExists(int id) => _context.Rentals.Any(e => e.ID == id);
    }
}