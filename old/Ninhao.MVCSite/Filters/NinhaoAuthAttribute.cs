using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ninhao.MVCSite.Filters
{
    /*四个过滤器
     * 1. 身份校验
     * 2. Action
     * 3. resault
     * 4. Exception
     */
    public class NinhaoAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            //when user saved in cookie and not in session, sync cookie into session
            if (filterContext.HttpContext.Request.Cookies["loginName"] != null && 
                filterContext.HttpContext.Session["loginName"] == null)
            {
                filterContext.HttpContext.Session["loginName"] = filterContext.HttpContext.Request.Cookies["loginName"].Value;
                filterContext.HttpContext.Session["userid"] = filterContext.HttpContext.Request.Cookies["userid"].Value;
            }
            //base.OnAuthorization(filterContext);
            if (!(filterContext.HttpContext.Session["loginName"]!=null || 
                filterContext.HttpContext.Request.Cookies["loginName"]!=null))
            {
                filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary()
                {
                    {"controller", "Home" },
                    {"action", "Login" }
                });
            }
        }
    }
}