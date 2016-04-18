using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace TheWorld.Services
{
    public class CoOrdService
    {
        private ILogger<CoOrdService> _logger;

        public CoOrdService(ILogger<CoOrdService> logger)
        {
            _logger = logger;
        }

        public async Task<CoOrdServiceResult> LookUp(string location)
        {
            var result = new CoOrdServiceResult()
            {
                Success = false,
                Message = "Undetermined Failure while looking up co-ordinates"
            };

            string encodedName = WebUtility.UrlEncode(location);
            string bingKey = Startup.Configuration["AppSettings:BingKey"];
            string url = String.Format(@"http://dev.virtualearth.net/REST/v1/Locations?q={0}&key={1}", encodedName, bingKey);

            var client = new HttpClient();
            string json = await client.GetStringAsync(url);

            // Read out the results
            // Fragile, might need to change if the Bing API changes
            var results = JObject.Parse(json);
            var resources = results["resourceSets"][0]["resources"];
            if (!resources.HasValues)
            {
                result.Message = $"Could not find '{location}' as a location";
            }
            else
            {
                var confidence = (string)resources[0]["confidence"];
                if (confidence != "High")
                {
                    result.Message = $"Could not find a confident match for '{location}' as a location";
                }
                else
                {
                    var coords = resources[0]["geocodePoints"][0]["coordinates"];
                    result.Latitude = (double)coords[0];
                    result.Longitude = (double)coords[1];
                    result.Success = true;
                    result.Message = "Success";
                }
            }
            return result;
        }
    }
}
