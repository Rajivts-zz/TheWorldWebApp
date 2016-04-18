using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using TheWorld.Models;
using System.Net;
using AutoMapper;
using Microsoft.Extensions.Logging;
using TheWorld.ViewModel;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace TheWorld.Controllers.Api
{
    [Route("api/trips")]
    public class TripController : Controller
    {
        private ILogger<TripController> _logger;
        private IWorldRepository _repository;

        public TripController(IWorldRepository repository, ILogger<TripController> logger)
        {
            _logger = logger;
            _repository = repository;
        }
        [HttpGet("")]
        public JsonResult Get()
        {
            IEnumerable<Trip> trips = _repository.GetUserTripsWithStops(User.Identity.Name);
            var results = Mapper.Map<IEnumerable<TripViewModel>>(trips);
            return Json(results);
        }

        [HttpPost("")]
        public JsonResult Post([FromBody]TripViewModel tripViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {                    
                    var trip = Mapper.Map<Trip>(tripViewModel);
                    trip.UserName = User.Identity.Name;
                    _logger.LogInformation("Attempting to save a new trip");
                    _repository.AddTrip(trip);

                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<TripViewModel>(trip));
                    }                    
                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Failed", ModelState = ModelState });
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = e.Message});
            }
        }
    }
}
