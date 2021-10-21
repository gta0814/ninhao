using CarPool.Authentication;
using CarPool.Mailer;
using CarPool.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace CarPool.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticateController : Controller
    {
        private readonly UserManager<User> userManager;
        private readonly RoleManager<UserRoles> roleManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext _context;
        private readonly SignInManager<User> _signInManager;
        private readonly IMailer _mailer;

        public AuthenticateController(ApplicationDbContext context, 
            UserManager<User> userManager, 
            RoleManager<UserRoles> roleManager, 
            SignInManager<User> signInManager, 
            IConfiguration configuration, 
            IMailer mailer)
        {
            _context = context;
            this.userManager = userManager;
            this.roleManager = roleManager;
            _configuration = configuration;
            _mailer = mailer;
            _signInManager = signInManager;
        }



        [HttpPost("SignIn")]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            var user = userManager.Users.SingleOrDefault(u => u.UserName == model.Email);
            if (user is null)
            {
                return NotFound(new Response { Success = false, Message = "User not found", Data = null });
            }

            var userSigninResult = await userManager.CheckPasswordAsync(user, model.Password);

            if (userSigninResult)
            {
                var roles = await userManager.GetRolesAsync(user);
                var recievedRequests = _context.RideRequests.Where(i => i.Trip.Vehicle.UserId == user.Id && i.IsDriverRead==false && i.IsApproved == null && i.Trip.TimeLeave>=DateTime.Now && i.IsActive == true).Count();
                var sentRequests = _context.RideRequests.Where(i => i.UserId == user.Id && i.IsPassengerRead == false && i.Trip.TimeLeave >= DateTime.Now && i.IsActive == true && (i.IsApproved == true || i.IsApproved == false)).Count();

                var code = GenerateJwt(user, roles);
                HttpContext.Response.Cookies.Append("token", code, new CookieOptions { HttpOnly = false });
                HttpContext.Response.Cookies.Append("email", user.Email, new CookieOptions { HttpOnly = false });
                HttpContext.Response.Cookies.Append("fullName", user.FullName, new CookieOptions { HttpOnly = false });
                HttpContext.Response.Cookies.Append("profilePicture", user.ImageURL, new CookieOptions { HttpOnly = false });
                HttpContext.Response.Cookies.Append("unreadReceived", recievedRequests.ToString(), new CookieOptions { HttpOnly = false });
                HttpContext.Response.Cookies.Append("unreadSent", sentRequests.ToString(), new CookieOptions { HttpOnly = false });
                return Ok(new Response { Success = true, Message = "", Data = new LoginResponse { Token = code, Email = user.Email, Name = user.FullName, ImageURL = user.ImageURL, UnreadReceived = recievedRequests, UnreadSent = sentRequests } });
            }


            return BadRequest(new Response { Success = false, Message = "Email or password incorrect.", Data = null });
        }

        [HttpPost("SignUp")]
        public async Task<IActionResult> SignUp([FromBody] SignUpModel model)
        {
            var user = new User()
            {
                Email = model.Email,
                UserName = model.Email,
                FullName = model.FullName,
                ImageURL = "/Images/profile.jpg",
                SecurityStamp = Guid.NewGuid().ToString()
            };

            var userCreateResult = await userManager.CreateAsync(user, model.Password);

            if (userCreateResult.Succeeded)
            {
                var code = await userManager.GenerateEmailConfirmationTokenAsync(user);

                var confirmationLink = Url.Action("ConfirmEmail", "Authenticate", new { token = code, email = user.Email }, Request.Scheme);

            //    await _mailer.SendEmailAsyc(user.Email, user.FullName, "Email Confirmation", "Welcome! Kindly Confirm Your email. " + confirmationLink);
                return Ok(new Response { Success = true, Message = "User Created!", Data = null });
            }

            return StatusCode(500, new Response { Success = false, Message = userCreateResult.Errors.First().Description, Data = null });
        }
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            Request.Cookies["token"].Remove(0);
            return Redirect("~/");
        }


       
        [Authorize]
        [HttpGet("GetUserData")]
        public async Task<ActionResult<Trip>> GetUserData()
        {
            var user = await userManager.GetUserAsync(HttpContext.User);
            var recievedRequests = _context.RideRequests.Where(i => i.Trip.Vehicle.UserId == user.Id && i.IsDriverRead == false && i.IsApproved == null && i.Trip.TimeLeave >= DateTime.Now && i.IsActive == true).Count();
            var sentRequests = _context.RideRequests.Where(i => i.UserId == user.Id && i.IsPassengerRead == false && i.Trip.TimeLeave >= DateTime.Now && i.IsActive == true && (i.IsApproved == true || i.IsApproved == false)).Count();

            return Ok(new Response { Success = true, Message = "", Data = new LoginResponse { UnreadReceived = recievedRequests, UnreadSent = sentRequests } });
        }

        [HttpPost("PasswordForget")]
        public async Task<IActionResult> PasswordForget(string email)
        {
            var user = await userManager.FindByEmailAsync(email);
            if (user != null)
            {

                var code = await userManager.GeneratePasswordResetTokenAsync(user);
                var confirmationLink = Request.Scheme + "://" + Request.Host + Url.Page("/ResetPassword", new { code = code });
                await _mailer.SendEmailAsyc(user.Email, user.FullName, "Reset Password","Kindly reset your password "+ confirmationLink);
                return Ok(new Response { Success = true, Message = "Check your email.", Data = null });

            }
            else
            {
                return BadRequest(new Response { Data = null, Message = "User not found", Success = false });
            }

        }

        [HttpGet("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string token, string email)
        {
            string StatusMessage;
            if (token == null || token == null)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.NotFound,
                    Content = "<html><body><h3>Not Found!</h3></body></html>"
                };
            }

            var user = await userManager.FindByEmailAsync(email);
            if (user == null)
            {
                return new ContentResult
                {
                    ContentType = "text/html",
                    StatusCode = (int)HttpStatusCode.BadRequest,
                    Content = "<html><body><h3>Unable to load user with ID '{userId}'.</h3></body></html>"
                };

            }

            var result = await userManager.ConfirmEmailAsync(user, token);
            StatusMessage = result.Succeeded ? "Thank you for confirming your email." : "Error confirming your email.";
            return new ContentResult
            {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = "<html><body><h3>" + StatusMessage + "</h3></body></html>"
            };
        }


        private string GenerateJwt(User user, IList<string> roles)
        {
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
            };

            var roleClaims = roles.Select(r => new Claim(ClaimTypes.Role, r));
            claims.AddRange(roleClaims);

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Secret"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
            var expires = DateTime.Now.AddDays(Convert.ToDouble(_configuration["JWT:ExpirationInDays"]));

            var token = new JwtSecurityToken(
                issuer: _configuration["JWT:ValidIssuer"],
                audience: _configuration["JWT:ValidIssuer"],
                claims,
                expires: expires,
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
