﻿using Ninhao.DAL;
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
        /// can either take id from driver with rest null or provide all to get car info.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public static async Task<Car> GetCar(Guid? id)
        {
            using (var carSvc = new CarService())
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
        }
        public static async Task<Car> GetCar(string make, string carmodel, string type, string color)
        {
            using (var carSvc = new CarService())
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
        /// <summary>
        /// this will only save if not exist in database
        /// </summary>
        /// <param name="make"></param>
        /// <param name="carmodel"></param>
        /// <param name="type"></param>
        /// <param name="color"></param>
        /// <returns></returns>
        public static async Task<Guid?> SaveCar(string make, string carmodel, string type, string color)
        {
            using (var carSvc = new CarService())
            {
                if (!await carSvc.GetAll().AnyAsync(m => m.Make == make && m.CarModel == carmodel && m.Type == type && m.Color == color))
                {
                    var car = new Car()
                    {
                        Make = make,
                        CarModel = carmodel,
                        Type = type,
                        Color = color
                    };
                    await carSvc.CreateAsync(car);
                    return car.Id;
                }
                else
                {
                    return null;
                }
            }
        }
    }
}
