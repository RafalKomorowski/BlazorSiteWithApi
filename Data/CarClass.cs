using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class CarClass : ModelBase
    {
        public int VehicleClassIndex { get; set; }

        [StringLength(64)]
        public string VehicleClass { get; set; }
    }
}
