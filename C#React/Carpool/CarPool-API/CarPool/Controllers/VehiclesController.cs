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

namespace CarPool.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class VehiclesController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<User> _userManager;
        public VehiclesController(ApplicationDbContext context, UserManager<User> userManager )
        {
            _userManager = userManager;
            _context = context;
        }

       
        [HttpGet("GetAll")]
        public async Task<ActionResult<IEnumerable<Vehicle>>> GetAll()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var cars =  _context.Vehicles.Where(i=>i.UserId == user.Id && i.IsActive == true).OrderByDescending(i=>i.CreateDate).AsNoTracking();
            return Ok(new Response { Success = true, Message = null, Data = cars });

        }

     
        [HttpGet("GetById{id}")]
        public async Task<ActionResult<Vehicle>> GetById(long id)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);
            var car = await _context.Vehicles.Where(i => i.UserId == user.Id && i.Id == id && i.IsActive == true).FirstOrDefaultAsync();

            if (car == null)
            {
                return NotFound(new Response { Success = false, Message = "Data Not Found!", Data = null});
            }

            return Ok(new Response { Success = true, Message = null, Data = car });
        }

       

        [HttpPost("AddNew")]
        public async Task<ActionResult<Vehicle>> AddNew(Vehicle vehicle)
        {
            var u = await _userManager.GetUserAsync(HttpContext.User);
            vehicle.UserId = u.Id;
            _context.Vehicles.Add(vehicle);
            await _context.SaveChangesAsync();

            return Ok(new Response { Success = true, Message = "Data Saved!", Data = vehicle });
        }

        
        [HttpPost("Deactive")]
        public async Task<IActionResult> Deactive(long id)
        {
            var car = await _context.Vehicles.FindAsync(id);
            if (car == null)
            {
                return NotFound(new Response { Success = false, Message = "Data not found!", Data = null });
            }

            car.IsActive = false;
            await _context.SaveChangesAsync();
            return Ok(new Response { Success = true, Message = "Deleted!", Data = null });
        }
       

        private bool VehicleExists(long id)
        {
            return _context.Vehicles.Any(e => e.Id == id);
        }
    }
}
