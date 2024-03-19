using FastEndpoints;
using FastEndpoints.Swagger;
using RainfallApi.Clients;
using RainfallApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddHttpClient<IRainfallApiClient, RainfallApiClient>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("RainfallApi:BaseUrl")!));

var info = builder.Configuration.GetOpenApiInfo();

ArgumentNullException.ThrowIfNull(info);

builder.Services.AddAuthorization();
builder.Services.AddFastEndpoints()
                .SwaggerDocument(o =>
                {
                    o.DocumentSettings = s =>
                    {
                        s.Title = info.Title;
                        s.Description = $"{info.Description} - {info.Contact.Name} - {info.Contact.Url}";
                        s.Version = info.Version;
                    };
                });

var app = builder.Build();

app.UseAuthorization();
app.UseFastEndpoints()
   .UseSwaggerGen();

app.Run();

