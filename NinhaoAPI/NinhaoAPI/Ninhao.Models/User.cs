﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.Models
{
    public class User : BaseEntity
    {
        [Required]
        [StringLength(maximumLength: 40), Column(TypeName = "varchar")]
        public string Email { get; set; }

        [Required]
        [StringLength(maximumLength: 30), Column(TypeName = "varchar")]
        public string Password { get; set; }

        [StringLength(maximumLength: 300), Column(TypeName = "varchar")]
        public string ImagePath { get; set; }
        /// <summary>
        /// Media contact ex: 微信, facebook
        /// </summary>
        public string Contact { get; set; }
        public int Phone { get; set; }

    }
}