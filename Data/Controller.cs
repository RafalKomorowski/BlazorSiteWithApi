using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Controller : ModelBase
    {
        [StringLength(8)]
        public string ControllerName { get; set; }
    }
}
