using Ninao.WebAPI.Filter;
using Ninao.WebAPI.Models;
using Ninhao.BLL;
using Ninhao.MVCSite.Models.UserViewModels;
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
    [MyAuth]
    [EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        public IHttpActionResult Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.ErrorData("数据不合法");
            }
            if (UserManager.Login(model.LoginName, model.Password, out Guid userid))
            {
                return this.SendData(JwtTools.Encode(new Dictionary<string, object>()
                {
                    {"username", model.LoginName },
                    {"userid", userid }
                }));
            }
            else
            {
                return this.ErrorData("Invaild email or password");
            }
        }
        [AllowAnonymous, HttpPost]
        [Route("register")]
        public async Task<IHttpActionResult> RegisterAsync(RegisterViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return this.ErrorData("invalid information");
            }
            await UserManager.Register(model.Email, model.Password, model.FirstName, model.LastName, model.NickName, model.age, model.Gender, null, model.Contact, model.Phone, model.Address, model.CarPlate, model.Make, model.CarModel, model.Type, model.Color);
            return Json(model);
        }
    }
}
