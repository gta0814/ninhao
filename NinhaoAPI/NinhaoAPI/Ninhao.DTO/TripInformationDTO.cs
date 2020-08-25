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
        public int AvailiableSeat { get; set; }
        public decimal? Price { get; set; }
        public string Note { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public string SocialAccount { get; set; }
        public int? Phone { get; set; }
        public string CarMake { get; set; }
        public string CarColor { get; set; }
        public string CarPlate { get; set; }
        public string CarType { get; set; }
    }
}
