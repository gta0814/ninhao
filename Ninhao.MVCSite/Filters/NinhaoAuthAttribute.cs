﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Ninhao.MVCSite.Filters
{
    public class NinhaoAuthAttribute : AuthorizeAttribute
    {
        public override void OnAuthorization(AuthorizationContext filterContext)
        {
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