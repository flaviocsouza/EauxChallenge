using Business.Interfaces.Repositories;
using Repository.Repository;
using AutoMapper;
using EauxWebApi.AutoMapper;
using Business.Services;
using Business.Interfaces.Services;
using Business.Interfaces.Notificatios;
using Business.Notifications;
using Microsoft.AspNetCore.Mvc;
using Repository.Context;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();

//Configure Db Connection
builder.Services.AddDbContext<EauxDbContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(Program));

//Supress ModelState Filter To Improve Validation
builder.Services.Configure<ApiBehaviorOptions>(options => options.SuppressModelStateInvalidFilter = true);

//Dependency Injection
/// Repositories
builder.Services.AddScoped<IProjectRepository, ProjectRepository>();
builder.Services.AddScoped<IWorkItemRepository, WorkItemRepository>();
/// Services
builder.Services.AddScoped<IProjectService, ProjectService>();
builder.Services.AddScoped<IWorkItemService, WorkItemService>();
///Notificator
builder.Services.AddScoped<INotificator, Notificator>();

var mappingConfig = new MapperConfiguration(config => config.AddProfile(new EauxProfile()));


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
