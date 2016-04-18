using AutoMapper;
using Microsoft.AspNet.Authorization;
using Microsoft.AspNet.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TheWorld.Models;
using TheWorld.Services;
using TheWorld.ViewModel;

namespace TheWorld.Controllers.Api
{
    [Route("api/trips/{tripName}/stops")]
    [Authorize]
    public class StopController : Controller
    {
        private CoOrdService _coordService;
        private ILogger<StopController> _logger;
        private IWorldRepository _repository;

        public StopController(IWorldRepository repository, ILogger<StopController> logger, CoOrdService coordService)
        {
            _repository = repository;
            _logger = logger;
            _coordService = coordService;
        }

        [HttpGet]
        public JsonResult Get(string tripName)
        {
            try
            {
                var results = _repository.GetTripByName(tripName, User.Identity.Name);
                if (results == null)
                    return Json(null);
                return Json(Mapper.Map<IEnumerable<StopViewModel>>(results.Stops.OrderBy(s => s.Order)));
            }            
            catch (Exception e)
            {
                _logger.LogError(e.Message, e);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Error occurred finding Trip name");
            }
        }

        [HttpPost]
        public async Task<JsonResult> Post(string tripName, [FromBody]StopViewModel stopViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _logger.LogInformation("Attempting to save incoming stop...");
                    var stop = Mapper.Map<Stop>(stopViewModel);

                    var coordResult = await _coordService.LookUp(stop.Name);

                    if (!coordResult.Success)
                    {
                        Response.StatusCode = (int)HttpStatusCode.BadRequest;
                        return Json(coordResult.Message);
                    }

                    stop.Latitude = coordResult.Latitude;
                    stop.Longitude = coordResult.Longitude;
                    _repository.AddStop(tripName, User.Identity.Name, stop);
                    if (_repository.SaveAll())
                    {
                        Response.StatusCode = (int)HttpStatusCode.Created;
                        return Json(Mapper.Map<StopViewModel>(stop));
                    }

                }
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json(new { Message = "Stop validation failed", ModelState = ModelState });
            }
            catch (Exception e)
            {
                _logger.LogError("Failed to save new stop, due to the following execption: " + e.Message, e);
                Response.StatusCode = (int)HttpStatusCode.BadRequest;
                return Json("Failed to save new stop");
            }
        }
    }
}
