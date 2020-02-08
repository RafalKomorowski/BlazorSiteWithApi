using System.ComponentModel.DataAnnotations;

namespace TodoApi.Models
{
    public class SessionMode : ModelBase
    {
        [StringLength(32)]
        public string SessionModeName { get; set; }
    }
}
