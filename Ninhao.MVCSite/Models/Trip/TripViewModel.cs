using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Ninhao.MVCSite.Models.Trip
{
    public class TripViewModel
    {
        public Guid? Id { get; set; }
        [Required]
        public string StartFrom { get; set; }
        [Required]
        public string Destination { get; set; }
        [Required]
        public DateTime TimeLeave { get; set; }
        [Required]
        public int AvailiableSeat { get; set; }
        public decimal? Price { get; set; }
        public string Note { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string SocialAccount { get; set; }
        public long? Phone { get; set; }
        public string CarMake { get; set; }
        public string CarColor { get; set; }
        public string CarPlate { get; set; }
        public string CarType { get; set; }
    }
}