using Base.Api.EndPoints.Authentication;
using Base.Api.EndPoints.Formalities;
using Base.Api.EndPoints.Test;
using Base.Api.Middlewares;
using Base.Infrastructure.IoC.DependencyInjection;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.RegisterDataBase(builder.Configuration)
    .RegisterLibraries()
    .RegisterProviders(builder.Configuration)
    .RegisterServices(builder.Configuration)
    .RegisterRepositories();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Api Base",
        Version = "v1"
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// builder.Services.AddCors(options =>
// {
//     options.AddPolicy("CORSPolicy",
//         b => b
//             .AllowAnyMethod()
//             .AllowAnyHeader()
//             .AllowCredentials()
//             .SetIsOriginAllowed((hosts) => true));
// });

// app.UseCors("CORSPolicy");
app.UseHttpsRedirection();
app.UseGlobalExceptionMiddleware();

// var summaries = new[]
// {
//     "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
// };
//
// app.MapGet("/weatherforecast", () =>
//     {
//         var forecast = Enumerable.Range(1, 5).Select(index =>
//                 new WeatherForecast
//                 (
//                     DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
//                     Random.Shared.Next(-20, 55),
//                     summaries[Random.Shared.Next(summaries.Length)]
//                 ))
//             .ToArray();
//         return forecast;
//     })
//     .WithName("GetWeatherForecast")
//     .WithOpenApi();


// app.MapTestEndpoints();
app.MapAuthEndpoints();
app.MapCitizenRequestEndpoints();
app.Run();

// record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
// {
//     public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
// }