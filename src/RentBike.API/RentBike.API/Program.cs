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
// Add services to the container.
builder.Services.AddControllers();
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
builder.Services.AddScoped<IRentPlanRepository, RentPlanRepository>();
builder.Services.AddScoped<IRentRepository, RentRepository>();
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);
builder.Services.AddHealthChecks();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    using var serviceScope = app.Services.GetService<IServiceScopeFactory>()!.CreateScope();
    var context = serviceScope.ServiceProvider.GetRequiredService<DataContext>();
    context.Database.EnsureCreated();
}

app.UseHealthChecks("/health");

app.UseMiddleware<ErrorHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
