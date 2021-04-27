﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ninao.WebAPI.Models
{
    public class LoginViewModel
    {
        [Required]
        public string LoginName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}