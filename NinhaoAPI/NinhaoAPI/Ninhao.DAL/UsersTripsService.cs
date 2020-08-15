using Ninhao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.DAL
{
    public class UsersTripsService: BaseService<Models.UsersTrips>
    {
        public UsersTripsService() : base(new NinhaoContext())
        {
        }
    }
}
