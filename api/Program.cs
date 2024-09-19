using api.Data;
using api.Interfaces;
using api.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Get the connection string from appsettings.json
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Register the DbContext with the connection string
builder.Services.AddDbContext<ApplicationDBContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers();
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IBookingRepository, BookingRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapControllers(); // Map API controllers if you have them


app.Run();

