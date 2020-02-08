using System;
using TodoApi.Areas.Identity.Data;

namespace TodoApi.Models
{
    public class LapTimes : ModelBase
    {
        public ProjectCars2User ProjectCars2User { get; set; }

        public Car Car { get; set; }

        public Track Track { get; set; }

        public double LapTime { get; set; }

        public DateTime LapDate { get; set; }

        public double Sector1 { get; set; }

        public double Sector2 { get; set; }

        public double Sector3 { get; set; }

        public int TrackTemp { get; set; }

        public int AmbTemp { get; set; }

        public double RainDensity { get; set; }

        public SessionMode SessionMode { get; set; }

        public Platform Platform { get; set; }

        public Setup Setup { get; set; }

        public Controller Controller { get; set; }

        public Camera Camera { get; set; }
    }
}
