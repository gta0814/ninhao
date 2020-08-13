using Ninhao.DAL;
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
        public static async Task CreateTrip(string startfrom, string destination, DateTime timeLeave, int seats, decimal price, Guid driverid, Guid passengerid)
        {
            using(var tripSvc = new TripService())
            {
                await tripSvc.CreateAsync(new Trip()
                {
                    StartFrom = startfrom,
                    Destination = destination,
                    TimeLeave = timeLeave,
                    AvailiableSeat = seats,
                    PricePerSeat = price,
                    DriverId = driverid,
                    PassengerId = passengerid
                });
            }
        }
        public static async Task EditTrip()
        {

        }
    }
}
