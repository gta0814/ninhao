using Ninhao.IDAL;
using Ninhao.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.DAL
{
    public class UserService : BaseService<Models.User>
    {
        public UserService() : base(new NinhaoContext())
        {
        }
        public async Task ChangeDriverCar(Guid id, Guid carid)
        {
            var user = new User() { Id = id};
            _db.Entry(user).State = EntityState.Unchanged;
            user.CarId = carid;
            await SaveAsync();
        }
    }
}
