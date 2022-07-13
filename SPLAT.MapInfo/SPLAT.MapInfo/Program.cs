using SPLAT.MapInfo.GoogleMapInfo.Api;
using SPLAT.MapInfo.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddTransient<GoogleDistanceApi>();
builder.Services.AddControllers();
builder.Services.AddGrpc();
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

app.MapGrpcService<DistanceInfoService>();
app.MapControllers();

app.Run();
