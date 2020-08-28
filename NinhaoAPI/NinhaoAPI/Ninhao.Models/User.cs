using System;
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
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string NickName { get; set; }
        public int? age { get; set; }
        [Required]
        [StringLength(maximumLength: 10), Column(TypeName = "varchar")]
        public string Gender { get; set; }

        [StringLength(maximumLength: 300), Column(TypeName = "varchar")]
        public string ImagePath { get; set; }
        /// <summary>
        /// Media contact ex: 微信, facebook
        /// </summary>
        [StringLength(maximumLength: 30), Column(TypeName = "varchar")]
        public string SocialMediaAccount { get; set; }
        public long Phone { get; set; }
        public string Address { get; set; }

        [StringLength(maximumLength: 10), Column(TypeName = "varchar")]
        public string CarPlate { get; set; }

        [ForeignKey(nameof(Car))]
        public Guid? CarId { get; set; }
        public Car Car { get; set; }

    }
}
