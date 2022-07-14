using SPLAT.Monolith.Common.Enums;

namespace SPLAT.Monolith.Services
{
    public interface IDistanceInfoSvc
    {
        Task<(int, DistanceUnit)> GetDistanceAsync(string originCity,
            string destinationCity);
    }
}
