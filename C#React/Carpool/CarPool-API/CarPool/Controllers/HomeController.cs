using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarPool.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace CarPool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;
        public HomeController(ApplicationDbContext context, IConfiguration configuration, UserManager<User> userManager)
        {
            _context = context;
            _configuration = configuration;
            _userManager = userManager;
        }

       
        [HttpGet("GetTrips")]
        public async Task<ActionResult<IEnumerable<Trip>>> GetTrips(string destination, 
                                                                    string origin, 
                                                                    DateTime? leaveDate,
                                                                    int? pageIndex)
        {
            //  var trips = from t in _context.Trips select t;
            var trips = _context.Trips.Where(i => i.TimeLeave >= DateTime.Now && i.IsActive == true)
                                      .Include(i => i.Vehicle)
                                      .ThenInclude(i => i.User).AsQueryable();
           
            if (!string.IsNullOrEmpty(destination))
            {
               
                trips = trips.Where(i => i.Destination == destination);
            }
            if(!string.IsNullOrEmpty(origin))
            {
                trips = trips.Where(i => i.Origin == origin);
            }
            if (leaveDate.HasValue)
            {
                var nd = leaveDate.Value.AddDays(1);
                trips = trips.Where(i => i.TimeLeave >= leaveDate && i.TimeLeave < nd);
            }
            int pageSize = Convert.ToInt32(_configuration["Paginated:PageSize"]);
            var d = await PaginatedList<Trip>.CreateAsync(trips.AsNoTracking(), pageIndex ?? 1, pageSize);
            
            return Ok(new Response { Data = new { items = d, HasNextPage = d.HasNextPage, PageIndex = d.PageIndex, TotalPages = d.TotalPages }, Success = true });            
        }

     
        [HttpGet("GetTripById")]
        public async Task<ActionResult<Trip>> GetTrip(long id)
        {
            var u = await _userManager.GetUserAsync(HttpContext.User);
            var isLogin = false;
            var isRequested = false;
            var isUserTrip = false;
            var trip = await _context.Trips.Where(i => i.Id == id)
                                            .Include(i => i.Riders)
                                            .ThenInclude(i=>i.User)
                                            .Include(i => i.Vehicle)
                                            .ThenInclude(i => i.User).FirstAsync();

            if (trip == null)
            {
                return NotFound(new Response { Success = false, Message = "Data not Found!"});
            }
            if (u != null)
            {
                isLogin = true;
                isRequested = await _context.RideRequests.Where(i => i.TripId == trip.Id && i.UserId == u.Id).AnyAsync();
                if(trip.Vehicle.UserId == u.Id)
                {
                    isUserTrip = true;
                }
            }

            return Ok( new Response { Data = new { isLogin, isRequested, isUserTrip, detail = trip }, Success = true });
        }

       
    }
}
