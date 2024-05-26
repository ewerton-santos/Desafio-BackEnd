using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RentBike.API.Middlewares;
using RentBike.Application.Services;
using RentBike.Application.Services.Interfaces;
using RentBike.Domain;
using RentBike.Domain.Repositories;
using RentBike.Infrastructure;
using RentBike.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);
IConfigurationRoot config = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
          .Build();
//IConfiguration _config = builder.Configuration;
//builder.Services.Configure<Config>(_config.GetSection("AppParameters"));
// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddSingleton<IConfig, Config>();
builder.Services.AddSingleton<IRabbitMQListenerService, RabbitMQListenerService>();
builder.Services.AddHostedService<RabbitMQListenerHostedService>();
builder.Services.AddScoped<IRabbitPublisherService, RabbitMQPublisherService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(options => options.UseNpgsql(config.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(c => c.RegisterServicesFromAssemblies(AppDomain.CurrentDomain.Load("RentBike.Application")));
builder.Services.AddScoped<IAdminUserRepository, AdminUserRepository>();
builder.Services.AddScoped<IDeliverymanUserRepository, DeliverymanUserRepository>();
builder.Services.AddScoped<IBikeRepository, BikeRepository>();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
