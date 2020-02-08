using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using ProjectCars2.Data;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using TodoApi.Areas.Identity.Data;
using TodoApi.DTO;
using TodoApi.Models;

namespace ProjectCars2Api.Controllers
{
    #region LapTimeController
    [Route("api/[controller]")]
    [ApiController]
    public class LapTimeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public LapTimeController(ApplicationDbContext context)
        {
            _context = context;
        }
        #endregion

        // GET: api/TodoItems
        [HttpGet]
        public async Task<ActionResult<IEnumerable<LapTimes>>> GetTLapTimes()
        {
            return await _context.LapTimes.ToListAsync();
        }

        #region snippet_GetByID
        // GET: api/LapTime/5
        [HttpGet("{id}")]
        public async Task<ActionResult<LapTimes>> GetLapTime(int id)
        {
            var lapTime = await _context.LapTimes.FindAsync(id);

            if (lapTime == null)
            {
                return NotFound();
            }

            return lapTime;
        }
        #endregion

        #region snippet_Update
        // PUT: api/LapTime/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTodoItem(int id, JObject jsonResult)
        {
            //LapTimeDTO lapTimeDTO = JsonConvert.DeserializeObject<LapTimeDTO>(jsonResult.ToString());

            //if (id != lapTimeDTO.Id)
            //{
            //    return BadRequest();
            //}

            //_context.Entry(todoItem).State = EntityState.Modified;

            //try
            //{
            //    await _context.SaveChangesAsync();
            //}
            //catch (DbUpdateConcurrencyException)
            //{
            //    if (!TodoItemExists(id))
            //    {
            //        return NotFound();
            //    }
            //    else
            //    {
            //        throw;
            //    }
            //}

            return NoContent();
        }
        #endregion

        #region snippet_Create
        // POST: api/LapTime
        [HttpPost]
        public async Task<ActionResult<LapTimes>> PostLapTime(LapTimeDTO lapTimeDTO)
        {
            if(lapTimeDTO != null)
            {
                if (IsAnyNullOrEmpty(lapTimeDTO) == false)
                {
                    ProjectCars2User user = await _context.Users.FirstOrDefaultAsync(c => c.GamerTag == lapTimeDTO.GamerTag);
                    if (user != null)
                    {
                        CarClass carClass = await _context.CarClasses.FirstOrDefaultAsync(c => c.VehicleClass == lapTimeDTO.VehicleClass);
                        if (carClass == null)
                            carClass = await CreateCarClass(lapTimeDTO.VehicleClass);
                        if (carClass == null)
                            return null;

                        Car car = await _context.Cars.FirstOrDefaultAsync(c => c.VehicleName == lapTimeDTO.VehicleName);
                        if (car == null)
                            car = await CreateCar(lapTimeDTO.VehicleName, carClass);
                        if (car == null)
                            return null;

                        Track track = await _context.Tracks.FirstOrDefaultAsync(c => c.TrackLength == lapTimeDTO.TrackLength);
                        if (track == null)
                            track = await CreateTrack(lapTimeDTO.TrackLength, lapTimeDTO.TrackName);
                        if (track == null)
                            return null;

                        SessionMode sessionMode = await _context.SessionModes.FirstOrDefaultAsync(c => c.SessionModeName == lapTimeDTO.SessionMode);
                        if (sessionMode == null)
                            sessionMode = await CreateSessionMode(lapTimeDTO.SessionMode);
                        if (sessionMode == null)
                            return null;

                        Platform platform = await _context.Platforms.FirstOrDefaultAsync(c => c.PlatformName == lapTimeDTO.Platform);
                        if (platform == null)
                            platform = await CreatePlatform(lapTimeDTO.Platform);
                        if (platform == null)
                            return null;

                        Setup setup = await _context.Setups.FirstOrDefaultAsync(c => c.SetupName == lapTimeDTO.Setup);
                        if (setup == null)
                            setup = await CreateSetup(lapTimeDTO.Setup);
                        if (setup == null)
                            return null;

                        TodoApi.Models.Controller controller = await _context.Controllers.FirstOrDefaultAsync(c => c.ControllerName == lapTimeDTO.Controller);
                        if (controller == null)
                            controller = await CreateController(lapTimeDTO.Controller);
                        if (controller == null)
                            return null;

                        Camera camera = await _context.Cameras.FirstOrDefaultAsync(c => c.CameraName == lapTimeDTO.Camera);
                        if (camera == null)
                            camera = await CreateCamera(lapTimeDTO.Camera);
                        if (camera == null)
                            return null;

                        bool isModified = false;
                        LapTimes lapTime = await _context.LapTimes
                            .Include(l => l.Track)
                            .Include(l => l.Car)
                            .Include(l => l.ProjectCars2User)
                            .FirstOrDefaultAsync(l =>l.ProjectCars2User.Email == user.Email && l.Track.TrackName == track.TrackName && l.Car.VehicleName == car.VehicleName);
                        if (lapTime != null)
                        {
                            isModified = true;
                        }
                        else
                        {
                            lapTime = new LapTimes();
                            lapTime.LapTime = double.MaxValue;
                        }

                        if (lapTime.LapTime > lapTimeDTO.LapTime)
                        {
                            lapTime.ProjectCars2User = user;
                            lapTime.Camera = camera;
                            lapTime.Car = car;
                            lapTime.Controller = controller;
                            lapTime.Platform = platform;
                            lapTime.SessionMode = sessionMode;
                            lapTime.Setup = setup;
                            lapTime.Track = track;
                            lapTime.LapDate = lapTimeDTO.LapDate;
                            lapTime.AmbTemp = lapTimeDTO.AmbTemp;
                            lapTime.LapTime = lapTimeDTO.LapTime;
                            lapTime.RainDensity = lapTimeDTO.RainDensity;
                            lapTime.Sector1 = lapTimeDTO.Sector1;
                            lapTime.Sector2 = lapTimeDTO.Sector2;
                            lapTime.Sector3 = lapTimeDTO.Sector3;
                            lapTime.TrackTemp = lapTimeDTO.TrackTemp;
                        }

                        if (isModified == true)
                            _context.Entry(lapTime).State = EntityState.Modified;
                        else
                            _context.LapTimes.Add(lapTime);

                        await _context.SaveChangesAsync();

                        return CreatedAtAction(nameof(GetLapTime), new { id = lapTime.Id }, lapTime);
                    }
                    else
                        return null;
                }
                else
                {
                    return null;
                }
            }
            return null;
        }
        #endregion

        private async Task<Car> CreateCar(string vehicleName, CarClass vehicleClass)
        {
            if (!string.IsNullOrEmpty(vehicleName) && vehicleClass != null)
            {
                Car car = new Car() { VehicleName = vehicleName, VehicleClass = vehicleClass };
                _context.Cars.Add(car);
                await _context.SaveChangesAsync();
                return car;
            }
            else
                return null;
        }

        private async Task<CarClass> CreateCarClass(string vehicleClass)
        {
            if (!string.IsNullOrEmpty(vehicleClass))
            {
                CarClass carClass = new CarClass() { VehicleClass = vehicleClass };
                _context.CarClasses.Add(carClass);
                await _context.SaveChangesAsync();
                return carClass;
            }
            else
                return null;
        }

        private async Task<Track> CreateTrack(float trackLength, string trackName)
        {
            if (trackLength > 0 && !string.IsNullOrEmpty(trackName))
            {
                Track track = new Track() { TrackLength = trackLength, TrackName = trackName };
                _context.Tracks.Add(track);
                await _context.SaveChangesAsync();
                return track;
            }
            else
                return null;
        }

        private async Task<SessionMode> CreateSessionMode(string sessionModeName)
        {
            if (!string.IsNullOrEmpty(sessionModeName))
            {
                SessionMode sessionMode = new SessionMode() { SessionModeName = sessionModeName };
                _context.SessionModes.Add(sessionMode);
                await _context.SaveChangesAsync();
                return sessionMode;
            }
            else
                return null;
        }

        private async Task<Platform> CreatePlatform(string platformName)
        {
            if (!string.IsNullOrEmpty(platformName))
            {
                Platform platform = new Platform() { PlatformName = platformName };
                _context.Platforms.Add(platform);
                await _context.SaveChangesAsync();
                return platform;
            }
            else
                return null;
        }

        private async Task<Setup> CreateSetup(string setupName)
        {
            if (!string.IsNullOrEmpty(setupName))
            {
                Setup setup = new Setup() { SetupName = setupName };
                _context.Setups.Add(setup);
                await _context.SaveChangesAsync();
                return setup;
            }
            else
                return null;
        }

        private async Task<TodoApi.Models.Controller> CreateController(string controllerName)
        {
            if (!string.IsNullOrEmpty(controllerName))
            {
                TodoApi.Models.Controller controller = new TodoApi.Models.Controller() { ControllerName = controllerName };
                _context.Controllers.Add(controller);
                await _context.SaveChangesAsync();
                return controller;
            }
            else
                return null;
        }

        private async Task<Camera> CreateCamera(string cameraName)
        {
            if (!string.IsNullOrEmpty(cameraName))
            {
                Camera camera = new Camera() { CameraName = cameraName };
                _context.Cameras.Add(camera);
                await _context.SaveChangesAsync();
                return camera;
            }
            else
                return null;
        }

        private bool IsAnyNullOrEmpty(object myObject)
        {
            foreach (PropertyInfo pi in myObject.GetType().GetProperties())
            {
                if (pi.PropertyType == typeof(string))
                {
                    string value = (string)pi.GetValue(myObject);
                    if (string.IsNullOrEmpty(value))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}
