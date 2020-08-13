using Ninhao.IDAL;
using Ninhao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.DAL
{
    public class TripService : BaseService<Models.Trip>
    {
        public TripService() : base(new NinhaoContext())
        {
        }

        public async Task ChangeTripInfo(Trip trip)
        {
            var newTrip = new Trip() { Id = trip.Id };
            _db.Entry(newTrip).State = EntityState.Unchanged;
            newTrip.StartFrom = trip.StartFrom;
            newTrip.Destination = trip.Destination;
            newTrip.TimeLeave = trip.TimeLeave;
            await SaveAsync();
        }
    }
}
