using Microsoft.EntityFrameworkCore;
using UrfuService.Data;
using UrfuService.Data.Entities;
using UrfuService.Data.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>(o =>
    o.UseNpgsql(builder.Configuration.GetValue<string>("DB_CONNECTION_STRING")));
builder.Services.AddScoped<IRepository<BrsEntity>, Repository<BrsEntity>>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();