using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarPool.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using CarPool.Mailer;

namespace CarPool.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMailer _mailer;

        public TripsController(ApplicationDbContext context, UserManager<User> userManager, IMailer mailer)
        {
            _context = context;
            _userManager = userManager;
            _mailer = mailer;
        }

        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetAll()
        {
            var u = await _userManager.GetUserAsync(HttpContext.User);
            var trips = _context.Trips.Include(i=>i.Vehicle).Where(i => i.Vehicle.UserId == u.Id && i.IsActive == true && i.TimeLeave >= DateTime.Now).AsNoTracking();
            return Ok(new Response { Data = trips, Success = true });
        }


        [HttpGet("GetById")]
        public async Task<ActionResult<Trip>> GetById(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var trip = await _context.Trips.Where(i => i.Vehicle.UserId == user.Id && i.IsActive == true).FirstOrDefaultAsync();

            if (trip == null)
            {
                return NotFound(new Response { Success = false, Message = "Data not found", Data = null });
            }

            return Ok(new Response { Success = true, Data = trip });
        }


        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(long id, DateTime dateTime)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var trip = await _context.Trips.Where(i => i.Vehicle.UserId == user.Id && i.IsActive == true).FirstOrDefaultAsync();

            if (trip != null)
            {
                return BadRequest(new Response { Success = false, Message = "Data not found", Data = null });
            }
            if (dateTime < DateTime.Now)
            {
                return BadRequest(new Response { Success = false, Message = "You cannot set Past date", Data = null });
            }
            var oldDateTime = trip.TimeLeave;
            trip.TimeLeave = dateTime;
            await _context.SaveChangesAsync();
            var userTrip = _context.UserTrips.Include(i=>i.User).Where(i => i.TripId == trip.Id);
            foreach (var rider in userTrip)
            {
                if (rider.User.EmailConfirmed)
                {
                    await _mailer.SendEmailAsyc(rider.User.Email, rider.User.FullName, "Trip Time Changed!", "Your trip to " + trip.Destination + " On " + oldDateTime.ToString() + " has been changed to " + dateTime.ToString()+ " by organizer.");
                }
                rider.IsActive = false;
            }

            return Ok(new Response { Success = false, Message = "Data saved", Data = null });
        }

        [HttpPost("CreateNew")]
        public async Task<ActionResult<Trip>> CreateNew(Trip trip)
        {
            var u = await _userManager.GetUserAsync(HttpContext.User);
            trip.RemainingAvailiableSeats = trip.AvailiableSeats;
            _context.Trips.Add(trip);
            await _context.SaveChangesAsync();

            return Ok(new Response { Success = true, Message = "Data saved", Data = trip });
        }

        [HttpPost("CancelByDriver")]
        public async Task<IActionResult> CancelByDriver(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var trip = await _context.Trips.Where(i => i.Vehicle.UserId == user.Id && i.Id == id && i.IsActive == true).FirstOrDefaultAsync();

            if (trip == null)
            {
                return BadRequest(new Response { Success = false, Message = "Data not found", Data = null });
            }
            
        
            var userTrip = _context.UserTrips.Include(i=>i.User).Where(i => i.TripId == trip.Id);
            foreach (var rider in userTrip)
            {
                if (rider.User.EmailConfirmed)
                {
                    await _mailer.SendEmailAsyc(rider.User.Email, rider.User.FullName, "Trip Cancelled!", "Your trip to " + trip.Destination + " On " + trip.TimeLeave.ToString() + " has been cancelled by organizer.");
                }
                rider.IsActive = false;
            }
            trip.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok(new Response { Success = true, Message = "Trip Cancelled", Data = null });
        }
        [HttpGet("CancelByPassenger")]
        public async Task<IActionResult> CancelByPassenger(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var userTrip = await _context.UserTrips.Include(i=>i.Trip).ThenInclude(i=>i.Vehicle).ThenInclude(i=>i.User).Where(i => i.UserId == user.Id && i.IsActive == true && i.TripId == id).FirstOrDefaultAsync();

            if (userTrip == null)
            {
                return BadRequest(new Response { Success = false, Message = "Data not found", Data = null });
            }

            if(userTrip.Trip.Vehicle.User.EmailConfirmed)
            {
                if(string.IsNullOrEmpty(user.FullName) || user.FullName == "string")
                {
                    await _mailer.SendEmailAsyc(userTrip.Trip.Vehicle.User.Email, userTrip.Trip.Vehicle.User.FullName, "Trip Cancelled!", "A rider have cancelled the trip to " + userTrip.Trip.Destination + " which is scheduled On " + userTrip.Trip.TimeLeave.ToString());

                }
                else
                {
                    await _mailer.SendEmailAsyc(userTrip.Trip.Vehicle.User.Email, userTrip.Trip.Vehicle.User.FullName, "Trip Cancelled!",user.FullName +" has cancelled the trip to " + userTrip.Trip.Destination + " which is scheduled On " + userTrip.Trip.TimeLeave.ToString());
                }

            }
            userTrip.Trip.RemainingAvailiableSeats += userTrip.SeatBooked;
            userTrip.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok(new Response { Success = true, Message = "Trip Cancelled", Data = null });
        }
        private bool TripExists(long id)
        {
            return _context.Trips.Any(e => e.Id == id);
        }
    }
}
