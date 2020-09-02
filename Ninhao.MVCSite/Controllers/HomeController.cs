using Ninhao.BLL;
using Ninhao.MVCSite.Filters;
using Ninhao.MVCSite.Models.UserViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace Ninhao.MVCSite.Controllers
{
    public class HomeController : Controller
    {
        [NinhaoAuth]
        public ActionResult Index()
        {
            return View();
        }
        [AllowAnonymous]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            await UserManager.Register(model.Email, model.Password, model.FirstName, model.LastName, model.NickName, model.age, model.Gender, null, model.Contact, model.Phone, model.Address, model.CarPlate, model.Make, model.CarModel, model.Type, model.Color);
            return Content("Success");
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                Guid userid = new Guid();
                if (UserManager.Login(model.Email, model.LoginPwd, out userid))
                {
                    //session or cookie
                    if (model.RememberMe)
                    {
                        Response.Cookies.Add(new HttpCookie("loginName")
                        {
                            Value = model.Email,
                            Expires = DateTime.Now.AddDays(7)
                        });
                        Response.Cookies.Add(new HttpCookie("userid")
                        {
                            Value = userid.ToString(),
                            Expires = DateTime.Now.AddDays(7)
                        });
                    }
                    else
                    {
                        Session["loginName"] = model.Email;
                        Session["userid"] = userid;
                    }
                    var driver = UserManager.GetUserByEmail(model.Email);
                    
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                ModelState.AddModelError("", "Email and Password doesn't match our record");
            }
            return View(model);
        }
    }
}