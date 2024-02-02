using CarPool.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace CarPool.Models
{
    public class User :IdentityUser<Guid>
    {
        public String FullName { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? DateOfBirth { get; set; }

        public bool? Gender { get; set; }
        public string ImageURL { get; set; }
        public string Address { get; set; }

        [JsonIgnore]
        public virtual ICollection<Vehicle> Vehicles { get; set; }

        [JsonIgnore]
        public virtual ICollection<UserTrip> UserTrips { get; set; }

        [JsonIgnore]
        public virtual ICollection<RideRequest> UserRequests { get; set; }
    }

}
