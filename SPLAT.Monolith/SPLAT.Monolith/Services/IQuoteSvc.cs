using SPLAT.Monolith.Models;

namespace SPLAT.Monolith.Services
{
    public interface IQuoteSvc
    {
        Task<Quote> CreateQuote(string originCity, string destinationCity);
    }
}
