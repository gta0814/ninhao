using Ninhao.DAL;
using Ninhao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.BLL
{
    public class CarManager
    {
        public static async Task CreateCar(string make, string carmodel, string type, string color)
        {
            if (make != null)
            {
                using (var carSvc = new CarService())
                {
                    await carSvc.CreateAsync(new Car()
                    {
                        Make = make,
                        CarModel = carmodel,
                        Type = type,
                        Color = color
                    });
                }
            }
        }
        /// <summary>
        /// can either take id with rest null or provide all to get car info.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Car> GetCar(Guid? id, string make, string carmodel, string type, string color)
        {
            using (var carSvc = new CarService())
            {
                if (id == null)
                {
                    if (await carSvc.GetAll().AnyAsync(m => m.Id == id))
                    {
                        return await carSvc.GetAll().Where(m => m.Id == id).FirstAsync();
                    }
                    else
                    {
                        throw new ArgumentException(message: "Vechical not exist");
                    }
                }
                else
                {
                    if (await carSvc.GetAll().AnyAsync(m => m.Make == make && m.CarModel == carmodel && m.Type == type && m.Color == color))
                    {
                        return await carSvc.GetAll().Where(m => m.Make == make && m.CarModel == carmodel && m.Type == type && m.Color == color).FirstAsync();
                    }
                    else
                    {
                        throw new ArgumentException(message: "Vechical not exist");
                    }
                }
            }

        }
    }
}
