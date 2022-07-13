using Microsoft.AspNetCore.Mvc;
using SPLAT.MapInfo.GoogleMapInfo.Api;
using SPLAT.MapInfo.GoogleMapInfo.Models;

namespace SPLAT.MapInfo.Controllers
{
    [Route("api/[controller]")]
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class MapInfoController : ControllerBase
    {
        private readonly GoogleDistanceApi _googleDistanceApi;

        public MapInfoController(GoogleDistanceApi googleDistanceApi)
        {
            _googleDistanceApi = googleDistanceApi;
        }

        [HttpGet]
        public async Task<GoogleDistanceData> GetDistance(string originCity, 
            string destinationCity)
        {
            return await _googleDistanceApi.GetMapDistance(originCity, 
                destinationCity);
        }
    }
}
