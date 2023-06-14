using Solicitudes.Data;
using Solicitudes.Services;
using Microsoft.EntityFrameworkCore;
using Solicitudes.Workers;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddControllers();

// Agregar servicio
builder.Services.AddWindowsService();
// builder.Services.AddHostedService<ServiceA>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ClienteContext>(options =>
{
  var connectionString = builder.Configuration.GetConnectionString("WebApiDatabase");
  options.UseNpgsql(connectionString);
});

// Agregar los servicios
builder.Services.AddScoped<ClienteService>();
builder.Services.AddScoped<SolicitudService>();
builder.Services.AddScoped<PagoService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//   app.UseSwagger();
//   app.UseSwaggerUI();
// }
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.CreateDbIfNotExists();
app.Run();
