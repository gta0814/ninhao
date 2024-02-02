using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarPool.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using CarPool.Mailer;

namespace CarPool.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class RideRequestsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        private readonly IMailer _mailer;
        public RideRequestsController(ApplicationDbContext context, UserManager<User> userManager, IMailer mailer)
        {
            _context = context;
            _userManager = userManager;
            _mailer = mailer;
        }

       
        [HttpGet("GetAllReceived")]
        public async Task<ActionResult<IEnumerable<RideRequest>>> GetAllReceived()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var requests = _context.RideRequests.Include(i => i.User).Include(i => i.Trip).ThenInclude(i=>i.Vehicle).Where(i => i.Trip.Vehicle.UserId == user.Id && i.IsActive == true && i.Trip.IsActive == true && i.Trip.TimeLeave >= DateTime.Now).AsNoTracking();
            return Ok(new Response { Success = true, Data = requests });
        }

        [HttpGet("GetReceivedById")]
        public async Task<ActionResult<RideRequest>> GetReceivedById(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var rideRequest = _context.RideRequests.Include(i => i.User).Include(i => i.Trip).Where(i => i.Trip.Vehicle.UserId == user.Id && i.Id == id).AsNoTracking().FirstOrDefault();
           
            if (rideRequest == null)
            {
                return NotFound(new Response { Success = false, Message = "Data Not found" });
            }

            MarkDriverRead(id);
            return Ok(new Response { Data = rideRequest, Success = true });
        }

        [HttpPut("ApproveReceived")]
        public async Task<IActionResult> ApproveReceived(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var rideRequest = await _context.RideRequests.Include(i => i.User).Where(i => i.Id == id && i.Trip.Vehicle.UserId == user.Id && i.IsActive==true).FirstOrDefaultAsync();
            if (id != rideRequest.Id)
            {
                return NotFound(new Response { Success = false, Message = "Data Not found" });
            }
            var trip = await _context.Trips.FindAsync(rideRequest.TripId);
            if(trip == null)
            {
                return NotFound(new Response { Success = false, Message = "Data Not found" });

            }
            if(trip.AvailiableSeats < rideRequest.SeatRequested)
            {
                return Ok(new Response { Success = false, Message = "Request seats are greater then available seats" });
            }
            trip.RemainingAvailiableSeats -= rideRequest.SeatRequested;
            rideRequest.IsApproved = true;
          
            var userTrip = new UserTrip { TripId = trip.Id, UserId = rideRequest.UserId, SeatBooked = rideRequest.SeatRequested };
            _context.UserTrips.Add(userTrip);
            await _context.SaveChangesAsync();
            if(rideRequest.User.EmailConfirmed)
            {
                await _mailer.SendEmailAsyc(user.Email, user.FullName, "Request Approved (Trip to "+trip.Destination+")", String.Format("Dear {0}, Your request for the trip to {1} has been approved!",rideRequest.User.FullName,trip.Destination));
            }
            return Ok(new Response { Success = true, Message = "Request Approved" });
        }
        [HttpPut("RejectReceived")]
        public async Task<IActionResult> RejectReceived(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var rideRequest = await _context.RideRequests.Include(i=>i.User).Where(i => i.Id == id && i.Trip.Vehicle.UserId == user.Id && i.IsActive== true).FirstOrDefaultAsync();
            if (rideRequest==null)
            {
                return NotFound(new Response { Success = false, Message = "Data Not found" });
            }
            var trip = await _context.Trips.FindAsync(rideRequest.TripId);
            rideRequest.IsApproved = false;
          
            await _context.SaveChangesAsync();
            if (rideRequest.User.EmailConfirmed)
            {
                await _mailer.SendEmailAsyc(user.Email, user.FullName, "Request Rejected (Trip to " + trip.Destination + ")", String.Format("Dear {0}, Your request for the trip to {1} has been rejected by the trip organizer.", rideRequest.User.FullName, trip.Destination));
            }
            return Ok(new Response { Success = true, Message = "Request Rejected" });
        }

        [HttpGet("GetAllSent")]
        public async Task<ActionResult<IEnumerable<RideRequest>>> GetAllSent()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var requests = _context.RideRequests.Include(i => i.Trip)
                                                .ThenInclude(i=>i.Vehicle)
                                                .ThenInclude(i=>i.User)
                                                .Where(i => i.UserId == user.Id && i.IsActive == true && i.Trip.IsActive == true && i.Trip.TimeLeave>= DateTime.Now)
                                                .AsNoTracking();
            return Ok(new Response { Success = true, Data = requests });
        }
        [HttpGet("GetSentById")]
        public async Task<ActionResult<RideRequest>> GetSentById(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            var rideRequest = _context.RideRequests.Include(i => i.Trip).ThenInclude(i => i.Vehicle).ThenInclude(i => i.User).Where(i => i.UserId == user.Id && i.Id == id).AsNoTracking().FirstOrDefault();
            if (rideRequest == null)
            {
                return NotFound(new Response { Success = false, Message = "Data Not found" });
            }
            MarkPassengerRead(id);
            return Ok(new Response { Data = rideRequest, Success = true });
        }
        [HttpGet("Send")]
        public async Task<ActionResult<RideRequest>> Send(long id,int requestedSeats)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var trip = await _context.Trips.Where(i=>i.Id==id).Include(i=>i.Vehicle).FirstAsync();
            if(trip==null)
            {
                return NotFound(new Response { Success = false, Message = "Data Not found" });
            }
            var isUserTrip = await _context.UserTrips.Where(i => i.UserId == user.Id && i.TripId == id).AnyAsync();
            if(isUserTrip)
            {
                return BadRequest(new Response { Success = false, Message = "Your trip is already approved" });

            }
            var isUserAlreadyRequested = await _context.RideRequests.Where(i => i.TripId == id && i.UserId == user.Id).AnyAsync();
            if(isUserAlreadyRequested)
            {
                return BadRequest(new Response { Success = false, Message = "You have already requested" });
            }
            if (user.Id == trip.Vehicle.UserId)
            {
                return BadRequest(new Response { Success = false, Message = "You cannot make request to yourself" });

            }

            if(requestedSeats>trip.AvailiableSeats)
            {
                return BadRequest(new Response { Success = false, Message = "Requested seats are greater then available seats" });
            }
            var rideRequest = new RideRequest { TripId = id, UserId = user.Id, SeatRequested = requestedSeats };
            _context.RideRequests.Add(rideRequest);
            await _context.SaveChangesAsync();

            return Ok(new Response { Success = true, Message = "Request sent" });

        }
        
        [HttpPut("CancelSent")]
        public async Task<IActionResult> CancelSent(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var rideRequest = _context.RideRequests.Where(i => i.Id == id && i.UserId == user.Id).First();

            
            if (rideRequest == null)
            {
                return NotFound(new Response { Success = false, Message = "Data Not found" });
            }
            if(rideRequest.IsApproved == true)
            {
                return BadRequest(new Response { Success = false, Message = "You cannot cancel an approved request." });
            }
            rideRequest.IsActive = false;
            await _context.SaveChangesAsync();

            return Ok(new Response { Success = true, Message = "Request canceled" });
        }

        private bool RideRequestExists(long id)
        {
            return _context.RideRequests.Any(e => e.Id == id);
        }
        private void MarkDriverRead(long id)
        {
            var rideRequest = _context.RideRequests.Where(i => i.Id == id).First();
            if(rideRequest!=null)
            {
                rideRequest.IsDriverRead = true;
                _context.SaveChangesAsync();
            }
        }
        private void MarkPassengerRead(long id)
        {
            var rideRequest = _context.RideRequests.Where(i => i.Id == id).First();
            if (rideRequest != null)
            {
                rideRequest.IsPassengerRead = true;
                _context.SaveChangesAsync();
            }
        }
    }
}
