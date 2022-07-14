using SPLAT.Monolith.Common.Constants;
using SPLAT.Monolith.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddScoped(typeof(IDistanceInfoSvc), typeof(DistanceInfoSvc));
builder.Services.AddScoped(typeof(IQuoteSvc), typeof(QuoteSvc));

var distanceMicroserviceUrl = builder.Configuration.GetSection(MicroserviceNames.DistanceMicroservice).Value;

builder.Services.AddHttpClient(MicroserviceNames.DistanceMicroservice, client =>
{
    client.BaseAddress = new Uri(distanceMicroserviceUrl);
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

app.Run();
