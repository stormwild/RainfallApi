using FastEndpoints;
using FastEndpoints.Swagger;
using RainfallApi.Clients;
using RainfallApi.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
// builder.Services.AddSwaggerGen(c =>
// {
//     c.SwaggerDoc("v1", builder.Configuration.GetOpenApiInfo());
// });

builder.Services.AddHttpClient<IRainfallApiClient, RainfallApiClient>(client => client.BaseAddress = new Uri(builder.Configuration.GetValue<string>("RainfallApi:BaseUrl")!));

var info = builder.Configuration.GetOpenApiInfo();

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

// // Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }

app.UseAuthorization();
app.UseFastEndpoints()
   .UseSwaggerGen();

app.Run();

