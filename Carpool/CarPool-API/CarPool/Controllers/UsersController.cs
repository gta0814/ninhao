using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CarPool.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;

namespace CarPool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _env;
        private readonly UserManager<User> _userManager;
        public UsersController(ApplicationDbContext context, IWebHostEnvironment env, UserManager<User> userManager)
        {
            _context = context;
            _env = env;
            _userManager = userManager;
        }



        // GET: api/Users/5
        [HttpGet("GetUser")]
        public async Task<ActionResult<User>> GetUser()
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);


            if (user == null)
            {
                return BadRequest(new Response { Message = "User Not found", Success = false });

            }

            return Ok(new Response { Data = user, Success = true });
        }

        [HttpPost("UploadImage")]
        public async Task<ActionResult> UploadImage([FromForm] IFormFile file)
        {
            var user = await _userManager.GetUserAsync(HttpContext.User);

            if (file != null)
            {

                string path = Path.Combine(_env.WebRootPath, "Images/" + user.Id + "-" + file.FileName);
                using (var stream = new FileStream(path, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
                user.ImageURL = "/Images/" + user.Id + "-" + file.FileName;
                await _userManager.UpdateAsync(user);
                return Ok(new Response { Data = user.ImageURL, Success = true });
            }
            else
            {
                return BadRequest(new Response { Message = "Unable to upload image", Success = false });

            }
        }
        [HttpPut("Edit")]
        public async Task<IActionResult> Edit(User user)
        {
            var user1 = await _userManager.GetUserAsync(HttpContext.User);
            user1.FullName = user.FullName;
            user1.PhoneNumber = user.PhoneNumber;
            user1.Address = user.Address;
            user1.Gender = user.Gender;
            user1.DateOfBirth = user.DateOfBirth;





            var result = await _userManager.UpdateAsync(user1);
            if(result.Succeeded)
            {
                return Ok(new Response { Message = "Data Saved", Success = true });

            }
            else
            {
                return BadRequest(new Response { Success = false, Message = "Error: " + result.Errors.ToString() });
            }

        }


        private bool UserExists(Guid id)
        {
            return _context.Users.Any(e => e.Id == id);
        }
    }
}
