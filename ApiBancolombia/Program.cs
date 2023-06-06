using ApiAplication.Domain.Request;
using ApiAplication.Persistence;
using ApiAplication.Services.Implements;
using ApiAplication.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<LogDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQLConnection")));
builder.Services.AddSingleton<CountryClientConfig, CountryClientConfig>(config => new CountryClientConfig()
{
    BaseAdress = builder.Configuration.GetValue<string>("CountryClient:BaseAdress")
});
builder.Services.AddHttpClient<ICountryRequesService, CountryRequesService>();
builder.Services.AddScoped<ICountryService, CountryServices>();
builder.Services.AddScoped<ILogService, LogService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();

