using Ninhao.DAL;
using Ninhao.DTO;
using Ninhao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.BLL
{
    public class TripManager
    {
        public static async Task InitailTestTrips()
        {
            using (var tripSvc = new TripService())
            {
                await tripSvc.CreateAsync(new Trip()
                {
                    StartFrom = "Edmonton",
                    Destination = "Calgary",
                    TimeLeave = DateTime.Now,
                    AvailiableSeat = 4,
                    PricePerSeat = 25
                });
            }
        }
        public async Task<List<TripInformationDTO>> GetAllTrip()
        {
            using (var tripSvc = new TripService())
            {
                var trips = tripSvc.GetAll(m=>m.IsRemoved != true)
            }
        }
        public static async Task CreateTrip(Guid driverId, string startfrom, string destination, DateTime timeLeave, int seats, decimal price)
        {
            using (var tripSvc = new TripService())
            {
                var newTrip = new Trip()
                {
                    StartFrom = startfrom,
                    Destination = destination,
                    TimeLeave = timeLeave,
                    AvailiableSeat = seats,
                    PricePerSeat = price
                };
                await tripSvc.CreateAsync(newTrip);

                Guid tripId = newTrip.Id;
                using (var usersTripsSvc = new UsersTripsService())
                {
                    await usersTripsSvc.CreateAsync(new UsersTrips() 
                    {
                        UserId = driverId,
                        TripId = tripId,
                        IsDriver = true
                    });
                }
            }
        }
        public static async Task EditTrip(Trip trip)
        {
            using (var tripSvc = new TripService())
            {
                await tripSvc.EditAsync(trip);
            }
        }
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
    }
}
