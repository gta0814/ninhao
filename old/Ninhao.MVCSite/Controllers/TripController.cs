﻿using Ninhao.BLL;
using Ninhao.DTO;
using Ninhao.MVCSite.Models.Home;
using Ninhao.MVCSite.Models.Trip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ninhao.MVCSite.Controllers
{
    public class TripController : Controller
    {
        //private Guid _driverid;
        //public Guid driverid 
        //{
        //    get 
        //    {
        //        return (Guid)Session["userid"];
        //    }
        //    set 
        //    {
        //        _driverid = Guid.Parse(Session["userid"].ToString());
        //    }
        //}
        // GET: Trip
        [HttpGet]
        public async Task<ActionResult> TripList(IndexViewModel model)
        {
            var trips = await TripManager.GetAllTrips(model.From, model.Destination, model.Date);
            return View(trips);
        }
        [HttpGet]
        public async Task<ActionResult> MyTrips()
        {
            Guid driverid = Guid.Parse(Session["userid"].ToString());
            var trips = await TripManager.GetMyTrips(driverid);
            return View(trips);
        }
        [HttpGet]
        public ActionResult CreateTrip()
        {
            if (Session["loginName"] != null)
            {
                var email = Session["loginName"].ToString();

                var driver = UserManager.GetUserByEmail(email);
                var driverName = driver.FirstName + ", " + driver.LastName;

                var tripinfo = new TripInformationDTO();
                tripinfo.Gender = driver.Gender;
                tripinfo.Name = driver.NickName != null? driver.NickName : driverName;
                tripinfo.Phone = driver.Phone;
                tripinfo.CarPlate = driver.CarPlate;
                tripinfo.CarMake = driver.Make;
                tripinfo.CarModel = driver.CarModel;
                tripinfo.CarType = driver.CarType;
                tripinfo.CarColor = driver.Color;
                return View(tripinfo);
            }
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateTrip(TripViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return Content("您输入的信息没整好啊,回前一页吧...");
            }

            Guid driverid = Guid.NewGuid();

            var driver = new UserInformationDTO();

            //anonymous creating trip
            if (Session["loginName"] == null)
            {
                driver.Id = driverid;
                driver.NickName = model.Name;
                driver.Gender = model.Gender;
                driver.Contact = model.SocialAccount;
                driver.Phone = model.Phone;
                driver.CarPlate = model.CarPlate;
                driver.Make = model.CarMake;

                Guid? carid = await CarManager.SaveCar(model.CarMake, model.CarModel, model.CarType, model.CarColor);
                await UserManager.CreateAnonymousUser(driver, carid);
                await TripManager.CreateTrip(model.StartFrom, model.Destination, model.TimeLeave, model.AvailiableSeat, model.Price, model.Note, driverid);

                return RedirectToAction(nameof(TripList));
            }
            else
            {
                driverid = Guid.Parse(Session["userid"].ToString());
                Guid? carid = await CarManager.SaveCar(model.CarMake, model.CarModel, model.CarType, model.CarColor);

                await TripManager.CreateTrip(model.StartFrom, model.Destination, model.TimeLeave, model.AvailiableSeat, model.Price, model.Note, driverid);

                return RedirectToAction(nameof(MyTrips));
            }
        }

        [HttpGet]
        public async Task<ActionResult> EditTrip(Guid id)
        {

            return View();
        }
        //TODO: Jquery 判定car变更 如果变了就调用SaveCar和ChangeDriverCar
    }
}