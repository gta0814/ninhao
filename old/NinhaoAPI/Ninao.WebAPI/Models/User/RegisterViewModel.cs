using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ninhao.MVCSite.Models.UserViewModels
{
    public class RegisterViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
        [Required]
        [StringLength(50,MinimumLength = 6)]
        public string Password { get; set; }
        [Required]
        [Compare(nameof(Password))]
        public string ConfirmPassword { get; set; }
        public string Contact { get; set; }
        public long Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public int? age { get; set; }
        [Required]
        public string Gender { get; set; }
        public string Address { get; set; }
        public string CarPlate { get; set; }
        public string Make { get; set; }
        public string Color { get; set; }
        public string Type { get; set; }
        public string CarModel { get; internal set; }
    }
}