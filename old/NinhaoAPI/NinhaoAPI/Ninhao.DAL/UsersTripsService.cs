using Ninhao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.DAL
{
    public class UsersTripsService: BaseService<Models.UsersTrips>
    {
        public UsersTripsService() : base(new NinhaoContext())
        {
        }
        public async Task EditPassenger(Guid id, Guid passengerId)
        {
            var existTrip = new UsersTrips() { Id = id, UserId = passengerId };
            _db.Entry(existTrip).State = EntityState.Unchanged;
            existTrip.UserId = passengerId;
            await SaveAsync();
        }
        //public async Task EditPassenger(Guid tripId, Guid passengerId)
        //{
        //    var getUserTrip = (from x in _db.UsersTrips
        //                  where x.TripId == tripId && x.UserId == passengerId
        //                  select x).FirstOrDefault();
        //    var usertrip = _db.UsersTrips.Find(getUserTrip.Id);
        //    _db.Entry(usertrip).State = EntityState.Unchanged;
        //    usertrip.UserId = passengerId;
        //    await SaveAsync();
        //}
    }
}
