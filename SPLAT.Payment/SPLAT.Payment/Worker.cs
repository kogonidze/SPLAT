using MassTransit;

namespace SPLAT.Payment
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IBusControl _bus;
        private readonly IConfiguration _configuration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;
            _configuration = configuration;

            _bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost");

                cfg.ReceiveEndpoint("payment-service", e =>
                {
                    e.Consumer(() => new InvoiceCreatedConsumer(_configuration), c =>
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