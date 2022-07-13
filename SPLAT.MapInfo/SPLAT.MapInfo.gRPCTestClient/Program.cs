using Grpc.Net.Client;
using SPLAT.MapInfo.Protos;

var channel = GrpcChannel.ForAddress(new Uri("https://localhost:7022"));
var client = new DistanceInfo.DistanceInfoClient(channel);

var request = new Cities
{
    DestinationCity = "Los Angeles,CA",
    OriginCity = "Topeka,KS"
};


var response = await client.GetDistanceAsync(request);

Console.WriteLine(response.Miles);

Console.ReadKey();