using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;
using TodoApi.Models;

namespace ProjectCars2.Data
{
    public class LapTimesService
    {
        private readonly ApplicationDbContext _context;


        public LapTimesService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task<LapTimes[]> GetLapTimesAsync()
        {

            return Task.FromResult(
                _context.LapTimes
                .Include(l => l.Camera)
                .Include(l => l.Track)
                .Include(l => l.Car)
                .Include(l => l.ProjectCars2User)
                .Include(l => l.Controller)
                .ToArray()
                );

        }

        public Task<LapTimes[]> GetMyLapTimesAsync(string gamerTag)
        {
            return Task.FromResult(
                _context.LapTimes
                .Include(l => l.Camera)
                .Include(l => l.Track)
                .Include(l => l.Car)
                .Include(l => l.ProjectCars2User)
                .Include(l => l.Controller)
                .Where(l => l.ProjectCars2User.GamerTag == gamerTag)
                .ToArray()
                );
        }

        public Task<Track[]> GetTracksAsync()
        {
            return Task.FromResult(
                _context.Tracks
                .ToArray()
                );
        }

        public Task<Car[]> GetCarsAsync()
        {
            return Task.FromResult(
                _context.Cars.ToArray()
                );
        }
    }
}
