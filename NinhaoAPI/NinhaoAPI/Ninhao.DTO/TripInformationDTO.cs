using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.DTO
{
    public class TripInformationDTO
    {
        public Guid Id { get; set; }
        public string StartFrom { get; set; }
        public string Destination { get; set; }
        public DateTime TimeLeave { get; set; }
    }
}
