using BioTekno.Task.Business;
using BioTekno.Task.Models.Entities;
using BioTekno.Task.Models.Input;
using BioTekno.Task.Repositories;
using BioTekno.Task.Repositories.Abstract;
using BioTekno.Task.Repositories.Concrete;
using BioTekno.Task.Services;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Serilog.Events;
using Serilog;
using StackExchange.Redis;
using System.Text.Json.Serialization;
using BioTekno.Task.Extensions;
using BioTekno.Task.Middleware;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x =>
                x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.ConfigureSql(builder.Configuration);
builder.Services.ConfigureMassTransit(builder.Configuration);
builder.Services.ConfigureRedis(builder.Configuration);
builder.Services.ConfigureComponents();

builder.Host.UseSerilog((context, config) =>
{
    config.WriteTo.Console();
    config.WriteTo.File("log.txt");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseExceptionMiddleware();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();