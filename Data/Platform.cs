using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TodoApi.Models
{
    public class Platform : ModelBase
    {
        [StringLength(3)]
        public string PlatformName { get; set; }
    }
}
