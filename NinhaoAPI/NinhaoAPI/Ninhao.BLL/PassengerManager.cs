using Ninhao.DAL;
using Ninhao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.BLL
{
    /// <summary>
    /// Passenger use "User" Entity and connect trip through UsersTrips
    /// </summary>
    
    //UsersTripManger
    public class PassengerManager
    {
        public static async Task AddPassenger(Guid tripId, Guid passengerId)
        {
            using (var usersTripsSvc = new UsersTripsService())
            {
                await usersTripsSvc.CreateAsync(new UsersTrips()
                {
                    TripId = tripId,
                    UserId = passengerId
                });
            }
        }
        //confirm by the driver
        //public static async Task AddPassengers(Guid tripId, Guid[] passengerIds)
        //{
        //    using (var usersTripsSvc = new UsersTripsService())
        //    {
        //        foreach (var passengerId in passengerIds)
        //        {
        //            await usersTripsSvc.CreateAsync(new UsersTrips()
        //            {
        //                TripId = tripId,
        //                UserId = passengerId
        //            }, false);
        //        }
        //        await usersTripsSvc.SaveAsync();
        //    }
        //}
        public static async Task EditPassenger(Guid tripId, Guid passengerId)
        {
            using (var usersTripsSvc = new UsersTripsService())
            {
                await usersTripsSvc.CreateAsync(new UsersTrips()
                {
                    TripId = tripId,
                    UserId = passengerId
                });
            }
        }
    }
}
