using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ninhao.Models
{
    public class Car : BaseEntity
    {
        [StringLength(maximumLength: 100), Column(TypeName = "varchar")]
        public string Make { get; set; }
        [StringLength(maximumLength: 40), Column(TypeName = "varchar")]
        public string Color { get; set; }
        [StringLength(maximumLength: 10), Column(TypeName = "varchar")]
        public string PlateNumber { get; set; }
        [StringLength(maximumLength: 40), Column(TypeName = "varchar")]
        public string Type { get; set; }
    }
}
