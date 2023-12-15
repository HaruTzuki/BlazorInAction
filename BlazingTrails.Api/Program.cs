using BlazingTrails.Api;
using BlazingTrails.Shared.Features.ManageTrails;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddDbContext<BlazingTrailsContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("BlazingTrailsContext")));

//builder.Services.AddValidatorsFromAssembly(Assembly.Load("BlazingTrails.Shared"));
builder.Services.AddValidatorsFromAssemblyContaining<TrailValidator>();

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
