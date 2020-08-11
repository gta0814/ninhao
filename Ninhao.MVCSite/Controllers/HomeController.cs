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
        [NinhaoAuth]
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        [NinhaoAuth]
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
        public async Task<ActionResult> Register(Models.UserViewModels.RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            IBLL.IUserManager userManager = new UserManager();
            await userManager.Register(model.Email, model.Password, model.Contact, model.Phone);
            return Content("Success");
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                IBLL.IUserManager userManager = new UserManager();
                if (await userManager.Login(model.Email, model.LoginPwd))
                {
                    //session or cookie
                    if (model.RememberMe)
                    {
                        Response.Cookies.Add(new HttpCookie("loginName")
                        {
                            Value = model.Email,
                            Expires = DateTime.Now.AddDays(7)
                        });
                    }
                    else
                    {
                        Session["loginName"] = model.Email;
                    }
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