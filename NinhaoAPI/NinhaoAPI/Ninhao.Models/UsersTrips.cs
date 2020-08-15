using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.Models
{
    public class UsersTrips : BaseEntity
    {
        [ForeignKey(nameof(User))]
        public Guid UserId { get; set; }
        public User User { get; set; }

        [ForeignKey(nameof(Trip))]
        public Guid TripId { get; set; }
        public Trip Trip { get; set; }

        /// <summary>
        /// 1 = Driver, 0 = Passenger
        /// </summary>
        public bool IsDriver { get; set; } = false;
    }
}
