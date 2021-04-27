using Ninao.WebAPI.Filter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace Ninao.WebAPI.Controllers
{
    /*  Prepare:
     *  1. get EF and config connection string
     *  2. get Jwt
     *  3. use "Attribute" to fillter/validate legaliztion of login
     *  4. Controllers cross route - Cors
     *  5. 为Action编写ViewModel用来validate post data的合法性
     *  6. 为返回的结果编写一个ResponseData处理统一返回的数据
     */

    [MyAuth]
    [EnableCors(origins:"*",headers:"*",methods:"*")]
    public class HomeController : ApiController
    {

    }
}
