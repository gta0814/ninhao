using Ninhao.DTO;
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

        //public async Task<List<TripInformationDTO>> GetAllFutureTrips()
        //{
        //    var trips = (from trip in _db.Trips
        //                 join x in _db.UsersTrips on trip.Id equals x.TripId
        //                 join driver in _db.Users on x.UserId equals driver.Id
        //                 where trip.IsRemoved != true && trip.TimeLeave >= DateTime.Today
        //                 select new TripInformationDTO
        //                 {
        //                     StartFrom = trip.StartFrom,
        //                     Destination = trip.Destination,
        //                     TimeLeave = trip.TimeLeave,
        //                     Price = trip.PricePerSeat,
        //                     AvailiableSeat = trip.AvailiableSeat,
        //                     FirstName = driver.FirstName,
        //                     NickName = driver.NickName,
        //                     Phone = driver.Phone,
        //                     SocialMediaAccount = driver.SocialMediaAccount,
        //                     Carid = driver.CarId,
        //                     ImagePath = driver.ImagePath
        //                 }).ToListAsync();
        //    return trips;
        //}
        public async Task EditPassenger(Guid id, Guid passengerId)
        {
            var newTrip = new UsersTrips() { Id = id };
            _db.Entry(newTrip).State = EntityState.Unchanged;
            newTrip.UserId = passengerId;
            await SaveAsync();
        }
    }
}
