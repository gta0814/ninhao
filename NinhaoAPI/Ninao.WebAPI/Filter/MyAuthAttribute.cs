using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Ninao.WebAPI.Filter
{
    public class MyAuthAttribute : Attribute, IAuthorizationFilter
    {
        public bool AllowMutiple { get; }

        public bool AllowMultiple => throw new NotImplementedException();

        public async Task<HttpResponseMessage> ExecuteAuthorizationFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            if (actionContext.ActionDescriptor.GetCustomAttributes<AllowAnonymousAttribute>(true).Count > 0)
            {
                return await continuation();
            }

            //获取request--> headers-->token
            IEnumerable<string> headers;
            if (actionContext.Request.Headers.TryGetValues("token", out headers))
            {
                //如果获取到headers里的token
                var loginName = JwtTools.Decode(headers.First())["username"].ToString();
                var UserId = Guid.Parse(JwtTools.Decode(headers.First())["userid"].ToString());
                (actionContext.ControllerContext.Controller as ApiController).User = new ApplicationUser(loginName, UserId);
                return await continuation();
            }

            return new HttpResponseMessage(HttpStatusCode.Unauthorized);
        }
    }
}