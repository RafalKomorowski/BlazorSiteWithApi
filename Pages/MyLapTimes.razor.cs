using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using ProjectCars2.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Areas.Identity.Data;
using TodoApi.Models;

namespace ProjectCars2.Pages
{
    public class MyLapTimesCode : ComponentBase
    {
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

        private void ApplyFilters()
        {
            lapTimes = lapTimesBackup;
            ApplyCarFilter();
            ApplyTrackFilter();
            StateHasChanged();
        }

        #endregion

        protected override async Task OnInitializedAsync()
        {
            var authState = await AuthenticationStateProvider.GetAuthenticationStateAsync();
            var user = await UserManager.GetUserAsync(authState.User);
            if (user != null && !string.IsNullOrEmpty(user.GamerTag))
            {
                lapTimes = (await lapTimesService.GetMyLapTimesAsync(user.GamerTag)).OrderBy(l => l.Track.TrackName).ThenBy(l => l.Car.VehicleName).ThenBy(l => l.LapTime).ToArray();

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
            }
        }
    }
}

