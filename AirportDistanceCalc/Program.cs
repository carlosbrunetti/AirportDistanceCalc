using AirportDistanceCalc.Api.Config;
using AirportDistanceCalc.Api.Config.AutoMapper;
using AirportDistanceCalc.Domain.Services;
using AirportDistanceCalc.Domain.Services.Interfaces;
using AirportDistanceCalc.Domain.Validations;
using AutoMapper;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

IMapper mapper = MappingConfig.RegisterMaps().CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddValidatorsFromAssemblyContaining<AirportCalcRequestValidation>();

var airportConfig = builder.Configuration.GetSection("AirportAPI").Get<AirportAPI>();

builder.Services.AddSingleton(airportConfig);
builder.Services.AddScoped<IAirportService, AirportService>();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();