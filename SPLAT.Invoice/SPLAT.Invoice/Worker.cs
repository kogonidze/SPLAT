using MassTransit;

namespace SPLAT.Invoice
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBusControl _bus;

        public Worker(ILogger<Worker> logger)
        {
            _logger = logger;

            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost");

                cfg.ReceiveEndpoint("invoice-service", e =>
                {
                    e.UseInMemoryOutbox();

                    e.Consumer<EventConsumer>(c =>
                        c.UseMessageRetry(m => m.Interval(5, new TimeSpan(0, 0, 10))));
                });
            });
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await _bus.StartAsync(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _bus.StopAsync(cancellationToken);
        }
    }
}