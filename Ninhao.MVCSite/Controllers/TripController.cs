using Ninhao.BLL;
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
        // GET: Trip
        [HttpGet]
        public async Task<ActionResult> TripList()
        {
            var trips = await TripManager.GetAllTrips();
            return View(trips);
        }
        [HttpGet]
        public ActionResult CreateTrip()
        {
            return View();
        }
        //[HttpPost]
        //public async Task<ActionResult> CreateTrip(TripViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return View(model);
        //    }

        //    await TripManager.CreateTrip()
        //    return View();
        //}
    }
}