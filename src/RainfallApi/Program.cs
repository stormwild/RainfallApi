using FastEndpoints;
using FastEndpoints.Swagger;
using Rainfall.Core.Clients;
using RainfallApi.Extensions;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IRainfallApiClient, RainfallApiClient>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("RainfallApi:BaseUrl")!));

builder.Services.AddFastEndpoints()
                .SwaggerDocument(o => o.SetOptions(builder));

var app = builder.Build();

app.UseFastEndpoints(c =>
    {
        c.Endpoints.Configurator = e => e.AllowAnonymous();
    })
   .UseSwaggerGen();

app.Run();
