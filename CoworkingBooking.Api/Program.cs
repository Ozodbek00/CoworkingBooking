using CoworkingBooking.Api.Configurations;
using CoworkingBooking.Api.Middlewares;
using CoworkingBooking.Data.DbContexts;
using CoworkingBooking.Data.Interfaces;
using CoworkingBooking.Data.Repositories;
using CoworkingBooking.Domain.Entities;
using CoworkingBooking.Service.Interfaces;
using CoworkingBooking.Service.Mappers;
using CoworkingBooking.Service.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.ConfigureJwt();
builder.Services.ConfigureSwaggerAuthorize();
builder.Services.AddScoped<IRepository<Branch>, Repository<Branch>>();
builder.Services.AddScoped<IRepository<Floor>, Repository<Floor>>();
builder.Services.AddScoped<IRepository<Table>, Repository<Table>>();
builder.Services.AddScoped<IRepository<Chair>, Repository<Chair>>();
builder.Services.AddScoped<IRepository<User>, Repository<User>>();
builder.Services.AddScoped<IRepository<Order>, Repository<Order>>();

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IBranchService, BranchService>();
builder.Services.AddScoped<IFloorService, FloorService>();
builder.Services.AddScoped<ITableService, TableService>();
builder.Services.AddScoped<IChairService, ChairService>();
builder.Services.AddScoped<IOrderService, OrderService>();


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

app.UseAuthentication();

app.UseAuthorization();

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.MapControllers();

app.Run();
