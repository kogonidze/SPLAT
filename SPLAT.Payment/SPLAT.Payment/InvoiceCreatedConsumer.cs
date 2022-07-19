using MassTransit;
using SPLAT.Messaging.Contracts;

namespace SPLAT.Payment
{
    public class InvoiceCreatedConsumer : IConsumer<InvoiceCreated>
    {
        public async Task Consume(ConsumeContext<InvoiceCreated> context)
        {
            
        }
    }
}
