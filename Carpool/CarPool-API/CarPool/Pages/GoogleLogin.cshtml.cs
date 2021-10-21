using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using CarPool.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using CarPool.Authentication;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;

namespace CarPool.Pages
{
    public class GoogleLoginModel : PageModel
    {
        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IEmailSender _emailSender;
        private readonly ILogger<GoogleLoginModel> _logger;
        private readonly ApplicationDbContext _context;
        private readonly IConfiguration _configuration;


        public GoogleLoginModel(
            SignInManager<User> signInManager,
            UserManager<User> userManager,
            ILogger<GoogleLoginModel> logger,
            IEmailSender emailSender,
            ApplicationDbContext context,
             IConfiguration configuration)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _logger = logger;
            _emailSender = emailSender;
            _context = context;
            _configuration = configuration;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ProviderDisplayName { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public string Message { get; set; }
        public class InputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }
        }


        public IActionResult OnGet()
        {
            string provider = "Google", returnUrl = null;
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./GoogleLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }
        public IActionResult OnPost()
        {
            string provider = "Google", returnUrl = null;
            // Request a redirect to the external login provider.
            var redirectUrl = Url.Page("./GoogleLogin", pageHandler: "Callback", values: new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }


        public async Task<IActionResult> OnGetCallbackAsync(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ErrorMessage = $"Error from external provider: {remoteError}";
                Message = ErrorMessage;
                return Page();
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                ErrorMessage = "Error loading external login information.";
                //    return RedirectToPage("./Login", new { ReturnUrl = returnUrl });
                Message = ErrorMessage;
                return Page();
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                _logger.LogInformation("{Name} logged in with {LoginProvider} provider.", info.Principal.Identity.Name, info.LoginProvider);
                var user = await _userManager.FindByLoginAsync(info.LoginProvider, info.ProviderKey);
                var roles = await _userManager.GetRolesAsync(user);
                var recievedRequests = _context.RideRequests.Where(i => i.Trip.Vehicle.UserId == user.Id && i.IsDriverRead == false && i.IsActive == true).Count();
                var sentRequests = _context.RideRequests.Where(i => i.UserId == user.Id && i.IsPassengerRead == false && i.IsActive == true).Count();
                var code = GenerateJwt(user, roles);
                HttpContext.Response.Cookies.Append("token", code, new CookieOptions { HttpOnly = false });
                HttpContext.Response.Cookies.Append("email", user.Email, new CookieOptions { HttpOnly = false });
                HttpContext.Response.Cookies.Append("fullName", user.FullName, new CookieOptions { HttpOnly = false });
                HttpContext.Response.Cookies.Append("profilePicture", user.ImageURL, new CookieOptions { HttpOnly = false });
                HttpContext.Response.Cookies.Append("unreadReceived", recievedRequests.ToString(), new CookieOptions { HttpOnly = false });
                HttpContext.Response.Cookies.Append("unreadSent", sentRequests.ToString(), new CookieOptions { HttpOnly = false });
                return Redirect("~/");
                //return new JsonResult(new Response { Success = true, Message = "", Data = new LoginResponse { Token = code, Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, ImageURL = user.ImageURL, ReceivedRequests = recievedRequests } });

            }
            if (result.IsLockedOut)
            {

                //    return RedirectToPage("./Lockout");
                Message = "User Lockout";
                return Page();
            }
            else
            {
                // If the user does not have an account, then ask the user to create an account.
                ReturnUrl = returnUrl;
                ProviderDisplayName = info.ProviderDisplayName;
                if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Email))
                {
                    Input = new InputModel
                    {
                        Email = info.Principal.FindFirstValue(ClaimTypes.Email)
                    };
                    string name = null, imageUrl = null;
                    //Start
                    if (info.Principal.HasClaim(c => c.Type == ClaimTypes.Name))
                    {
                        name = info.Principal.FindFirstValue(ClaimTypes.Name);
                    }
                    if (info.Principal.HasClaim(c => c.Type == "picture"))
                    {
                        imageUrl = info.Principal.FindFirstValue("picture");
                    }
                    else
                    {
                        imageUrl = "/Images/profile.jpg";
                    }
                    var user = new User { UserName = Input.Email, Email = Input.Email, FullName = name, ImageURL = imageUrl };

                    var result1 = await _userManager.CreateAsync(user);
                    if (result1.Succeeded)
                    {
                        result1 = await _userManager.AddLoginAsync(user, info);
                        if (result1.Succeeded)
                        {
                            _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);




                            var roles = await _userManager.GetRolesAsync(user);
                            var recievedRequests = _context.RideRequests.Where(i => i.Trip.Vehicle.UserId == user.Id && i.IsDriverRead == false && i.IsActive == true).Count();
                            var sentRequests = _context.RideRequests.Where(i => i.UserId == user.Id && i.IsPassengerRead == false && i.IsActive == true).Count();

                            var code = GenerateJwt(user, roles);
                            HttpContext.Response.Cookies.Append("token", code, new CookieOptions { HttpOnly = false });
                            HttpContext.Response.Cookies.Append("email", user.Email, new CookieOptions { HttpOnly = false });
                            HttpContext.Response.Cookies.Append("fullName", user.FullName, new CookieOptions { HttpOnly = false });
                            HttpContext.Response.Cookies.Append("profilePicture", user.ImageURL, new CookieOptions { HttpOnly = false });
                            HttpContext.Response.Cookies.Append("unreadReceived", recievedRequests.ToString(), new CookieOptions { HttpOnly = false });
                            HttpContext.Response.Cookies.Append("unreadSent", sentRequests.ToString(), new CookieOptions { HttpOnly = false });

                            return Redirect("~/");
                          //  return new JsonResult(new Response { Success = true, Message = "", Data = new LoginResponse { Token = GenerateJwt(user, roles), Email = user.Email, FirstName = user.FirstName, LastName = user.LastName, ImageURL = user.ImageURL, ReceivedRequests = recievedRequests } });

                        }
                    }
                    //End

                }
                Message = "Error Occure!";
                return Page();
               
            }
        }

        //public async Task<IActionResult> OnPostConfirmationAsync(string returnUrl = null)
        //{
        //    returnUrl = returnUrl ?? Url.Content("~/");
        //    // Get the information about the user from the external login provider
        //    var info = await _signInManager.GetExternalLoginInfoAsync();
        //    if (info == null)
        //    {
        //        ErrorMessage = "Error loading external login information during confirmation.";
        //        Message = ErrorMessage;
        //        return Page();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        var user = new User { UserName = Input.Email, Email = Input.Email };

        //        var result = await _userManager.CreateAsync(user);
        //        if (result.Succeeded)
        //        {
        //            result = await _userManager.AddLoginAsync(user, info);
        //            if (result.Succeeded)
        //            {
        //                _logger.LogInformation("User created an account using {Name} provider.", info.LoginProvider);

        //                var userId = await _userManager.GetUserIdAsync(user);
        //                var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
        //                code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
        //                var callbackUrl = Url.Page(
        //                    "/Account/ConfirmEmail",
        //                    pageHandler: null,
        //                    values: new { area = "Identity", userId = userId, code = code },
        //                    protocol: Request.Scheme);

        //                await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
        //                    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

        //                // If account confirmation is required, we need to show the link if we don't have a real email sender
        //                if (_userManager.Options.SignIn.RequireConfirmedAccount)
        //                {
        //                    return RedirectToPage("./RegisterConfirmation", new { Email = Input.Email });
        //                }

        //                await _signInManager.SignInAsync(user, isPersistent: false, info.LoginProvider);

        //                return LocalRedirect(returnUrl);
        //            }
        //        }
        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError(string.Empty, error.Description);
        //        }
        //    }

        //    ProviderDisplayName = info.ProviderDisplayName;
        //    ReturnUrl = returnUrl;
        //    return Page();
        //}
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
