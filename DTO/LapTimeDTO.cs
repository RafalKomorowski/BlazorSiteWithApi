using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.DTO
{
    [NotMapped]
    public class LapTimeDTO
    {
        [StringLength(64)]
        public string GamerTag { get; set; }

        [StringLength(64)]
        public string VehicleName { get; set; }

        [StringLength(64)]
        public string VehicleClass { get; set; }

        [StringLength(64)]
        public string TrackName { get; set; }

        public float TrackLength { get; set; }

        public double LapTime { get; set; }

        public double Sector1 { get; set; }

        public double Sector2 { get; set; }

        public double Sector3 { get; set; }

        public int TrackTemp { get; set; }

        public int AmbTemp { get; set; }

        public double RainDensity { get; set; }

        [StringLength(32)]
        public string SessionMode { get; set; }

        [StringLength(1)]
        public string ValidLap { get; set; }

        public DateTime LapDate { get; set; }

        [StringLength(3)]
        public string Platform { get; set; }

        [StringLength(8)]
        public string Setup { get; set; }

        [StringLength(8)]
        public string Controller { get; set; }

        [StringLength(8)]
        public string Camera { get; set; }
    }
}
