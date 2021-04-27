using Ninao.WebAPI.Filter;
using Ninhao.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Ninao.WebAPI.Controllers
{
    //[MyAuth]
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    //[RoutePrefix("api/trip")]
    //public class TripController : ApiController
    //{
    //    [HttpGet]
    //    public async Task<IHttpActionResult> TripList()
    //    {
    //        var trips = await TripManager.GetAllTrips();
    //        return Json(trips);
    //    }
    //    [HttpGet]
    //    public async Task<IHttpActionResult> MyTrips()
    //    {
    //        Guid driverid = Guid.Parse(Session["userid"].ToString());
    //        var trips = await TripManager.GetMyTrips(driverid);
    //        return Json(trips);
    //    }
    //    [HttpGet]
    //    public IHttpActionResult CreateTrip()
    //    {
    //        if (Session["loginName"] != null)
    //        {
    //            var email = Session["loginName"].ToString();

    //            var driver = UserManager.GetUserByEmail(email);
    //            var driverName = driver.FirstName + ", " + driver.LastName;

    //            var tripinfo = new TripInformationDTO();
    //            tripinfo.Gender = driver.Gender;
    //            tripinfo.Name = driver.NickName != null ? driver.NickName : driverName;
    //            tripinfo.Phone = driver.Phone;
    //            tripinfo.CarPlate = driver.CarPlate;
    //            tripinfo.CarMake = driver.Make;
    //            tripinfo.CarModel = driver.CarModel;
    //            tripinfo.CarType = driver.CarType;
    //            tripinfo.CarColor = driver.Color;
    //            return Json(tripinfo);
    //        }
    //        return Json();
    //    }
    //    [HttpPost]
    //    [ValidateAntiForgeryToken]
    //    public async Task<IHttpActionResult> CreateTrip(TripViewModel model)
    //    {
    //        if (!ModelState.IsValid)
    //        {
    //            return Content("您输入的信息没整好啊,回前一页吧...");
    //        }

    //        Guid driverid = Guid.NewGuid();

    //        var driver = new UserInformationDTO();

    //        //anonymous creating trip
    //        if (Session["loginName"] == null)
    //        {
    //            driver.Id = driverid;
    //            driver.NickName = model.Name;
    //            driver.Gender = model.Gender;
    //            driver.Contact = model.SocialAccount;
    //            driver.Phone = model.Phone;
    //            driver.CarPlate = model.CarPlate;
    //            driver.Make = model.CarMake;

    //            Guid? carid = await CarManager.SaveCar(model.CarMake, model.CarModel, model.CarType, model.CarColor);
    //            await UserManager.CreateAnonymousUser(driver, carid);
    //            await TripManager.CreateTrip(model.StartFrom, model.Destination, model.TimeLeave, model.AvailiableSeat, model.Price, model.Note, driverid);

    //            return RedirectToAction(nameof(TripList));
    //        }
    //        else
    //        {
    //            driverid = Guid.Parse(Session["userid"].ToString());
    //            Guid? carid = await CarManager.SaveCar(model.CarMake, model.CarModel, model.CarType, model.CarColor);

    //            await TripManager.CreateTrip(model.StartFrom, model.Destination, model.TimeLeave, model.AvailiableSeat, model.Price, model.Note, driverid);

    //            return RedirectToAction(nameof(MyTrips));
    //        }
    //    }

    //    [HttpGet]
    //    public async Task<IHttpActionResult> EditTrip(Guid id)
    //    {

    //        return Json();
    //    }
    //}
}
