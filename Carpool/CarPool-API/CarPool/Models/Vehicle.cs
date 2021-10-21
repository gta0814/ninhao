using CarPool.Authentication;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CarPool.Models
{
    public class Vehicle
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }


        [ForeignKey("User")]
       
        public Guid UserId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]

        public User User { get; set; }

        [Required(ErrorMessage ="Car Make Required")]
        [StringLength(maximumLength: 100), Column(TypeName = "varchar")]
        public string Make { get; set; }

        [Required(ErrorMessage = "Model Required")]
        [StringLength(maximumLength: 100), Column(TypeName = "varchar")]
        public string Model { get; set; }


        [StringLength(maximumLength: 40), Column(TypeName = "varchar")]
        public string Color { get; set; }


        [StringLength(maximumLength: 40), Column(TypeName = "varchar")]
        public string Type { get; set; }

        

        [StringLength(maximumLength: 100), Column(TypeName = "varchar")]
        public string Registration { get; set; }
 
        [HiddenInput]
        public DateTime CreateDate { get; set; } = DateTime.Now;

        [HiddenInput]
        public bool IsActive { get; set; } = true;


        [JsonIgnore]
        public virtual  ICollection<Trip> Trips { get; set; }
    }
}
