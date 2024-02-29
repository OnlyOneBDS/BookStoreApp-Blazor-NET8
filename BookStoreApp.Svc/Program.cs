using BookStoreApp.Svc.Configurations;
using BookStoreApp.Svc.Data;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Host.UseSerilog((context, config) =>
{
  config.WriteTo.Console().ReadFrom.Configuration(context.Configuration);
});

var connectionString = builder.Configuration.GetConnectionString("BookStoreDbConnection");
builder.Services.AddDbContext<BookStoreDbContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAutoMapper(typeof(MapperConfig));

builder.Services.AddCors(options => 
{
  options.AddPolicy("AllowAll", policy => 
    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin());
});

builder.Services.AddControllers();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  app.UseSwagger();
  app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAll");

app.UseAuthorization();

app.MapControllers();

app.Run();
