using MassTransit;
using SPLAT.Messaging.Contracts;
using SPLAT.Monolith.Common.Constants;
using SPLAT.Monolith.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped(typeof(IDistanceInfoSvc), typeof(DistanceInfoSvc));
builder.Services.AddScoped(typeof(IQuoteSvc), typeof(QuoteSvc));
builder.Services.AddScoped(typeof(IInvoiceSvc), typeof(InvoiceSvc));

var distanceMicroserviceUrl = builder.Configuration.GetSection(MicroserviceNames.DistanceMicroservice).Value;

builder.Services.AddHttpClient(MicroserviceNames.DistanceMicroservice, client =>
{
    client.BaseAddress = new Uri(distanceMicroserviceUrl);
});

builder.Services.AddMassTransit(x =>
{
    x.UsingRabbitMq((context, cfg) =>
    {
        cfg.Host("localhost");
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.MapPost(pattern: "/api/invoices",
    handler: async (int customerNumber, List<InvoiceItems> invoiceItems, IInvoiceSvc invoiceSvc) =>
    {
        await invoiceSvc.CreateInvoice(customerNumber, invoiceItems);

        return Results.Ok();
    });

app.Run();
