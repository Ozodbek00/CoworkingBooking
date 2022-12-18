using CoworkingBooking.Data.DbContexts;
using CoworkingBooking.Data.Interfaces;
using CoworkingBooking.Data.Repositories;
using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.Mappers;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IRepository<Branch>, Repository<Branch>>();
builder.Services.AddScoped<IRepository<Floor>, Repository<Floor>>();
builder.Services.AddScoped<IRepository<Table>, Repository<Table>>();
builder.Services.AddScoped<IRepository<Chair>, Repository<Chair>>();
builder.Services.AddScoped<IRepository<Student>, Repository<Student>>();
builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();

builder.Services.AddDbContext<CoworkingDBContext>(option =>
    option.UseNpgsql(builder.Configuration.GetConnectionString("CoworkingConnection")));

builder.Services.AddAutoMapper(typeof(MappingProfile));

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

app.UseAuthorization();

app.MapControllers();

app.Run();
