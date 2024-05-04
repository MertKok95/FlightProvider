using CoreWCF;
using CoreWCF.Channels;
using CoreWCF.Configuration;
using CoreWCF.Description;
using FlightProvider;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddServiceModelServices();
builder.Services.AddServiceModelMetadata();
builder.Services.AddSingleton<IServiceBehavior, UseRequestHeadersForMetadataAddressBehavior>();


var app = builder.Build();
app.UseServiceModel(bld =>
{
    bld.AddService<AirProvider>();
    bld.AddServiceEndpoint<AirProvider, IAirProvider>(new BasicHttpBinding(BasicHttpSecurityMode.Transport), "/AirProviderService.svc");
    var mb = app.Services.GetRequiredService<ServiceMetadataBehavior>();
    mb.HttpsGetEnabled = true;
});

//app.MapGet("/", () => "Hello World!");

app.Run();
