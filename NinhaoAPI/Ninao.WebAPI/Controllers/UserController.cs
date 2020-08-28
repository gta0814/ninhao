using Ninao.WebAPI.Filter;
using Ninao.WebAPI.Models;
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
    public class UserController : ApiController
    {
        //[AllowAnonymous]
        //[HttpPost]
        //public IHttpActionResult Login(LoginViewModel model)
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return this.ErrorData("数据不合法");
        //    }

        //}
    }
}
