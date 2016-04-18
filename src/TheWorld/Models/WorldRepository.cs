using Microsoft.Data.Entity;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheWorld.Models
{    
    public class WorldRepository : IWorldRepository
    {
        private WorldContext _context;
        private ILogger<WorldRepository> _logger;

        public WorldRepository(WorldContext context, ILogger<WorldRepository> logger)
        {
            _logger = logger;
            _context = context;
        }

        public void AddStop(string tripName, string username, Stop stop)
        {
            var trip = _context.Trips.Where(t => t.Name == tripName && t.UserName == username).FirstOrDefault();
            stop.Order = trip.Stops == null ? 1 : trip.Stops.Max(s => s.Order) + 1;
            if (trip.Stops == null)
                trip.Stops = new List<Stop>();
            trip.Stops.Add(stop);
            _context.Stops.Add(stop);
        }

        public void AddTrip(Trip trip)
        {
            _context.Add(trip);
        }

        public IEnumerable<Trip> GetAllTrips()
        {
            try
            {
                return _context.Trips.OrderBy(t => t.Name).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return null;
            }
        }

        public IEnumerable<Trip> GetAllTripsWithStops()
        {
            try
            {
                return _context.Trips.OrderBy(t => t.Name).Include(t => t.Stops).ToList();
            }            
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return null;
            }
        }

        public Trip GetTripByName(string tripName, string username)
        {
            return _context.Trips.Include(t => t.Stops).Where(t => t.Name == tripName && t.UserName == username).FirstOrDefault();
        }

        public IEnumerable<Trip> GetUserTripsWithStops(string name)
        {
            try
            {
                return _context.Trips.OrderBy(t => t.Name).Where(t => t.UserName == name).Include(t => t.Stops).ToList();
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                return null;
            }
        }

        public bool SaveAll()
        {
            return _context.SaveChanges() > 0;
        }
    }
}
