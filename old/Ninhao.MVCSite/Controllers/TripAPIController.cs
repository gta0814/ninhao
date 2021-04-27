using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Ninhao.MVCSite.Controllers
{
    public class TripAPIController : ApiController
    {
        // GET: api/TripAPI
        public IEnumerable<string> Get()
        {

            return new string[] { "value1", "value2" };
        }

        // GET: api/TripAPI/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/TripAPI
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/TripAPI/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/TripAPI/5
        public void Delete(int id)
        {
        }
    }
}
