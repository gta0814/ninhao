using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.DTO
{
    public class UserInformationDTO
    {
        public Guid Id { get; set; }
        public string Email { get; set; }

        public string ImagePath { get; set; }
        public string Contact { get; set; }
        public long? Phone { get; set; }
        public string FirstName { get; set; }
        public string NickName { get; set; }
        public int? Age { get; set; }
        public string Gender { get; set; }
        public string Address { get; set; }
        public string LastName { get; set; }
        public string Make { get; set; }
        public string CarModel { get; set; }
        public string CarType { get; set; }
        public string Color { get; set; }
        public string CarPlate { get; set; }
    }
}
