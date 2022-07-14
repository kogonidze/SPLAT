using SPLAT.Monolith.Models;

namespace SPLAT.Monolith.Services
{
    public class QuoteSvc : IQuoteSvc
    {
        private readonly IDistanceInfoSvc _distanceInfoSvc;

        public QuoteSvc(IDistanceInfoSvc distanceInfoSvc)
        {
            _distanceInfoSvc = distanceInfoSvc;
        }

        public async Task<Quote> CreateQuote(string originCity, string destinationCity)
        {
            var distanceInfo = await _distanceInfoSvc.GetDistanceAsync(originCity, destinationCity);

            var quote = new Quote
            {
                Id = 123,
                ExpectedDistance = distanceInfo.Item1,
                ExpectedDistanceUnit = distanceInfo.Item2
            };

            return quote;
        }
    }
}
