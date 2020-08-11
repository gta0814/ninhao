using Ninhao.IDAL;
using Ninhao.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.DAL
{
    public class UserService : BaseService<Models.User>, IUserService
    {
        public UserService() : base(new NinhaoContext())
        {
        }
    }
}
