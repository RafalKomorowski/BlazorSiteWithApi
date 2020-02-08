using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ProjectCars2.Data;
using TodoApi.Models;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using TodoApi.Areas.Identity.Data;
using Microsoft.JSInterop;

namespace ProjectCars2.Pages
{
    public class LapTimesCode : ComponentBase
    {
        [Inject]
        IJSRuntime JSRuntime { get; set; }

        [Inject]
        LapTimesService lapTimesService { get; set; }

        [Inject]
        AuthenticationStateProvider AuthenticationStateProvider { get; set; }

        [Inject]
        protected UserManager<ProjectCars2User> UserManager { get; set; }

        protected TodoApi.Models.LapTimes[] lapTimes;
        private TodoApi.Models.LapTimes[] lapTimesBackup;

        protected List<Track> Tracks = null;
        protected List<Car> Cars = null;
        protected List<ProjectCars2User> Users = null;

        protected string currentTrack = "";

        public object test { get; set; }

        #region Properties for filterring

        protected float? _trackFilter = null;
        protected float? TrackFilter
        {
            get
            {
                return _trackFilter;
            }
            set
            {
                _trackFilter = value;

                ApplyFilters();
            }
        }

        protected string _carFilter = "-- Select Car --";
        protected string CarFilter
        {
            get
            {
                return _carFilter;
            }
            set
            {
                _carFilter = value;

                ApplyFilters();
            }
        }

        protected string _userFilter = "-- Select User --";
        protected string UserFilter
        {
            get
            {
                return _userFilter;
            }
            set
            {
                _userFilter = value;

                ApplyFilters();
            }
        }

        private void ApplyCarFilter()
        {
            if (CarFilter != "-- Select Car --")
            {
                lapTimes = lapTimes.Where(l => l.Car.VehicleName == CarFilter).ToArray();
            }
        }

        private void ApplyTrackFilter()
        {
            if (TrackFilter >= 0)
            {
                lapTimes = lapTimes.Where(l => l.Track.TrackLength == TrackFilter).ToArray();
            }
        }

        private void ApplyUserFilter()
        {
            if (UserFilter != "-- Select User --")
            {
                lapTimes = lapTimes.Where(l => l.ProjectCars2User.GamerTag == UserFilter).ToArray();
            }
        }

        private void ApplyFilters()
        {
            lapTimes = lapTimesBackup;
            ApplyCarFilter();
            ApplyTrackFilter();
            ApplyUserFilter();
            StateHasChanged();
        }

        #endregion

        protected override async Task OnInitializedAsync()
        {
            lapTimes = (await lapTimesService.GetLapTimesAsync()).OrderBy(l => l.Track.TrackName).ThenBy(l => l.Car.VehicleName).ThenBy(l=>l.LapTime).ToArray();
            lapTimesBackup = lapTimes;

            Tracks = lapTimes
              .GroupBy(p => p.Track.TrackName)
              .Select(g => g.First().Track)
              .OrderBy(t =>t.TrackName)
              .ToList();

            Cars = lapTimes
             .GroupBy(p => p.Car.VehicleName)
             .Select(g => g.First().Car)
             .OrderBy(c => c.VehicleName)
             .ToList();

            Users = lapTimes
             .GroupBy(p => p.ProjectCars2User.GamerTag)
             .Select(g => g.First().ProjectCars2User)
             .OrderBy(u => u.GamerTag)
             .ToList();
        }
    }
}

