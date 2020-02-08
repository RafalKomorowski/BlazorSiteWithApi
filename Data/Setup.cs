using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class Setup : ModelBase
    {
        [StringLength(8)]
        public string SetupName { get; set; }
    }
}
