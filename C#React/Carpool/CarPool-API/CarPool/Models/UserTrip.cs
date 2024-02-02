using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System.Text.Json.Serialization;

namespace CarPool.Models
{
    public class UserTrip
    {


        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Int64 Id { get; set; }


        
        public Guid UserId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [ForeignKey("UserId")]
        public User User { get; set; }



      
        public Int64 TripId { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingDefault)]
        [ForeignKey("TripId")]
        public Trip Trip { get; set; }


        public int SeatBooked { get; set; }
        /// <summary>
        /// 1 = Driver, 0 = Passenger
        /// </summary>

        [HiddenInput]
        public DateTime CreateTime { get; set; } = DateTime.Now;

        [HiddenInput]
        public bool IsActive { get; set; } = true;


       
    }
}
