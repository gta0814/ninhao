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
    public class Trip
    {

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }

        [ForeignKey("Vehicle")]

        public Int64 VehicleId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Vehicle Vehicle { get; set; }

 

        [Required]
        public string Origin { get; set; }
      
        [Required]
        public string Destination { get; set; }


        [Required(ErrorMessage ="Leave Time is required.")]
        public DateTime TimeLeave { get; set; }
      

        [Required]
        [Range(0,32, ErrorMessage = "Available Seat(s) can only be between 0 to 32")]  
        public int AvailiableSeats { get; set; }

        [Required]
        [Range(0, 32, ErrorMessage = "Remaining Available Seat(s) can only be between 0 to 32")]
        public int RemainingAvailiableSeats { get; set; }


        [Column(TypeName = "decimal(18,2)")]
        public decimal PricePerSeat { get; set; }
        public string Note { get; set; }


        [HiddenInput]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [HiddenInput]
        public bool IsActive { get; set; } = true;


       
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<UserTrip> Riders { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public virtual ICollection<RideRequest> Requests { get; set; }

    }
}
