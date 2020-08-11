using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.Models
{
    public class Trip : BaseEntity
    {
        /// <summary>
        /// Start Location (city)
        /// </summary>
        [Required]
        [Column(TypeName = "varchar")]
        public string StartFrom { get; set; }
        /// <summary>
        /// Destination Location (city)
        /// </summary>
        [Required]
        [Column(TypeName = "varchar")]
        public string Destination { get; set; }
        /// <summary>
        /// Leaving time
        /// </summary>
        public DateTime TimeLeave { get; set; }
        /// <summary>
        /// Vechial space
        /// </summary>
        [Required]
        [IntegerValidator(MinValue = 0, MaxValue = 32)]
        public int AvailiableSeat { get; set; }
        /// <summary>
        /// Price per person
        /// </summary>
        public decimal? PricePerSeat { get; set; }
        /// <summary>
        /// Driver
        /// </summary>
        [ForeignKey(nameof(Driver))]
        public Guid DriverId { get; set; }
        public User Driver { get; set; }
        /// <summary>
        /// Passenger
        /// </summary>
        [ForeignKey(nameof(Passenger))]
        public Guid PassengerId { get; set; }
        public User Passenger { get; set; }
    }
}
