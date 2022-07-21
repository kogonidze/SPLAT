using MassTransit;
using SPLAT.Messaging.Contracts;
using System.Net.Http.Json;

namespace SPLAT.Payment
{
    public class InvoiceCreatedConsumer : IConsumer<InvoiceCreated>
    {
        private readonly IConfiguration _configuration;

        public InvoiceCreatedConsumer(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task Consume(ConsumeContext<InvoiceCreated> context)
        {
            var fakeBankApiUrl = _configuration["FakeBankApi"];
            var clientId = context.Message.InvoiceData.CustomerNumber;
            var price = CalculatePrice(context.Message.InvoiceData.InvoiceItems);

            using var client = new HttpClient();

            var request = new HttpRequestMessage(HttpMethod.Post, fakeBankApiUrl);

            request.Content = JsonContent.Create(new { ClientId = clientId, Price = price });

            var response = await client.SendAsync(request);
            response.EnsureSuccessStatusCode();
        }

        private double CalculatePrice(List<InvoiceItems> invoiceItems)
        {
            var price = 0.0;

            foreach (var item in invoiceItems)
            {
                price += item.Price;
            }

            return price;
        }
    }
}
