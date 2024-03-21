using FastEndpoints;
using FastEndpoints.Swagger;
using Rainfall.Api.Extensions;
using Rainfall.Core.Clients;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddHttpClient<IRainfallApiClient, RainfallApiClient>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("RainfallApi:BaseUrl")!));

builder.Services.AddProblemDetails();

builder.Services.AddFastEndpoints()
                .SwaggerDocument(o => o.SetOptions(builder));

var app = builder.Build();

app.UseFastEndpoints(c =>
    {
        c.Endpoints.Configurator = e => e.AllowAnonymous();
        c.Errors.UseProblemDetails();
    })
   .UseSwaggerGen();

if (app.Environment.IsProduction())
{
    app.UseExceptionHandler();
}

app.Run();

public partial class Program { }