using WeatherApi.Application.Services;
using WeatherApi.Domain.Interfaces;
using WeatherApi.Infrastructure.Repositories;
using Microsoft.OpenApi.Models;
using WeatherApi.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllers();
builder.Services.AddHttpClient<WeatherService>();
builder.Services.AddSingleton<IWeatherRepository>(new WeatherRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddTransient<WeatherService>();
builder.Services.AddHttpClient<IHttpClientWrapper, HttpClientWrapper>();

// Configure Swagger
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Weather API",
        Description = "API para obter informações climáticas de cidades e aeroportos",
        Contact = new OpenApiContact
        {
            Name = "Filipe Mateus dos Santos",
            Email = "filipemds@gmail.com",
            Url = new Uri("https://github.com/filipemds")
        },
    });

    // Optional: Include XML comments if you have them
    var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);
});

var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Weather API V1"));
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
});

app.Run();


//using WeatherApi.Application.Services;
//using WeatherApi.Domain.Interfaces;
//using WeatherApi.Infrastructure.Repositories;

//var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddControllers();
//builder.Services.AddHttpClient<WeatherService>();
//builder.Services.AddSingleton<IWeatherRepository>(new WeatherRepository(builder.Configuration.GetConnectionString("DefaultConnection")));
//builder.Services.AddSwaggerGen();

//var app = builder.Build();

//if (app.Environment.IsDevelopment())
//{
//    app.UseSwagger();
//    app.UseSwaggerUI();
//}

//app.UseRouting();
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});

//app.Run();
