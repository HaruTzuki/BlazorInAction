using BlazingTrails.Api;
using BlazingTrails.Shared.Features.ManageTrails.Shared;
using FluentValidation;
using FluentValidation.AspNetCore;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddFluentValidationClientsideAdapters();
builder.Services.AddDbContext<BlazingTrailsContext>(options => options.UseSqlite(builder.Configuration.GetConnectionString("BlazingTrailsContext")));

//builder.Services.AddValidatorsFromAssembly(Assembly.Load("BlazingTrails.Shared"));
builder.Services.AddValidatorsFromAssemblyContaining<TrailValidator>();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

if(app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}

app.UseHttpsRedirection();

app.UseBlazorFrameworkFiles();
app.UseStaticFiles();
app.UseStaticFiles(new StaticFileOptions()
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), @"Images")),
    RequestPath = new Microsoft.AspNetCore.Http.PathString("/Images")
});

app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
