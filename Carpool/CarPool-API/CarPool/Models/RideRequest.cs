using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace CarPool.Models
{
    public class RideRequest
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }


        [ForeignKey("User")]
        
        public Guid UserId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public User User { get; set; }


        [ForeignKey("Trip")]
        public Int64 TripId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        public Trip Trip { get; set; }

        [Required]
        public int SeatRequested { get; set; }

        [Required]
        public bool IsDriverRead { get; set; } = false;


        public bool? IsApproved { get; set; } 

        [Required]
        public bool IsPassengerRead { get; set; } = false;

        [HiddenInput]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [HiddenInput]
        public bool IsActive { get; set; } = true;
    }
}
