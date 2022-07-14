using SPLAT.Monolith.Common.Constants;
using SPLAT.Monolith.Common.Enums;
using SPLAT.Monolith.Models;
using System.Text.Json;

namespace SPLAT.Monolith.Services
{
    public class DistanceInfoSvc : IDistanceInfoSvc
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public DistanceInfoSvc(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        /// <summary>
        /// Call the microservice to retrieve distance between two cities
        /// </summary>
        /// <param name="originCity"></param>
        /// <param name="destinationCity"></param>
        /// <returns>Tuple for distance and the distance type.</returns>
        public async Task<(int, DistanceUnit)> GetDistanceAsync(string originCity,
            string destinationCity)
        {
            var httpClient = _httpClientFactory.CreateClient(MicroserviceNames.DistanceMicroservice);

            var mapMicroserviceUrl = $"?originCity={originCity}&destinationCity={destinationCity}";
            var responseStream = await httpClient.GetStreamAsync(mapMicroserviceUrl);

            var distanceData = await JsonSerializer.DeserializeAsync<MapDistanceInfo>(responseStream);
            var distance = 0;

            var distanceUnit = DistanceUnit.Unknown;

            foreach (var row in distanceData.rows)
            {
                foreach (var rowElement in row.elements)
                {
                    if (int.TryParse(CleanDistanceInfo(rowElement.distance.text), 
                        out var distanceConverted))
                    {
                        distance += distanceConverted;

                        if (rowElement.distance.text.EndsWith("mi"))
                        {
                            distanceUnit = DistanceUnit.Miles;
                        }

                        if (rowElement.distance.text.EndsWith("km"))
                        {
                            distanceUnit = DistanceUnit.Kilometers;
                        }
                    }
                }
            }

            return (distance, distanceUnit);
        }

        private string CleanDistanceInfo(string value)
        {
            return value
                .Replace("mi", string.Empty)
                .Replace("km", string.Empty)
                .Replace(",", string.Empty);
        }
    }
}
