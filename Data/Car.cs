using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Car : ModelBase
    {
        [StringLength(64)]
        public string VehicleName { get; set; }

        public CarClass VehicleClass { get; set; }
    }
}
