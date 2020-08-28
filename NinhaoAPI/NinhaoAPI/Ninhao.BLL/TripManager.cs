using Ninhao.DAL;
using Ninhao.DTO;
using Ninhao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.BLL
{
    public class TripManager
    {
        /// <summary>
        /// Get all trips information including drivers and cars
        /// </summary>
        /// <returns></returns>
        public static async Task<List<TripInformationDTO>> GetAllTrips()
        {
            using (var usertripSvc = new UsersTripsService())
            {
                 var result = await usertripSvc.GetAll(m => m.Trip.IsRemoved != true && m.IsDriver == true)
                                    .Include(m => m.Trip)
                                    .Include(m => m.User.Car)
                                    .Select(m => new TripInformationDTO()
                                    {
                                        StartFrom = m.Trip.StartFrom,
                                        Destination = m.Trip.Destination,
                                        TimeLeave = m.Trip.TimeLeave,
                                        AvailiableSeat = m.Trip.AvailiableSeat,
                                        Price = m.Trip.PricePerSeat,
                                        Note = m.Trip.Note,
                                        Name = m.User.NickName == null ? m.User.NickName : m.User.FirstName,
                                        Gender = m.User.Gender,
                                        SocialAccount = m.User.SocialMediaAccount,
                                        Phone = m.User.Phone,
                                        CarMake = m.User.Car.Make,
                                        CarModel = m.User.Car.CarModel,
                                        CarColor = m.User.Car.Color,
                                        CarPlate = m.User.CarPlate,
                                        CarType = m.User.Car.Type
                                    }).ToListAsync();
                if (result == null)
                {
                    return null;
                }
                else
                {
                    return result;
                }


            }
        }
        public static async Task CreateTrip(Trip trip, Guid driverId)
        {
            using (var tripSvc = new TripService())
            {
                var newTrip = new Trip()
                {
                    StartFrom = trip.StartFrom,
                    Destination = trip.Destination,
                    TimeLeave = trip.TimeLeave,
                    AvailiableSeat = trip.AvailiableSeat,
                    PricePerSeat = trip.PricePerSeat,
                    Note = trip.Note
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
        
    }
}
