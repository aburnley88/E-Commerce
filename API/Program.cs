using System.Reflection.PortableExecutable;
using Microsoft.EntityFrameworkCore;
using Infrastructure.Data;
using Core.Interfaces;
using API.Helpers;
using API.Middleware;
using Microsoft.AspNetCore.Mvc;
using API.Errors;
using API.Extensions;

var builder = WebApplication.CreateBuilder(args);
string connString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.



builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(MappingProfiles));

builder.Services.AddDbContext<StoreContext>(x => x.UseSqlite(connString)); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.AddApplicationServices();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", policy=>
    {
        policy.AllowAnyHeader().AllowAnyMethod().WithOrigins("https://localhost:7307");
    });
});

var app = builder.Build();
using(var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var loggerFactory = services.GetRequiredService<ILoggerFactory>();
    try
    {
        var context = services.GetRequiredService<StoreContext>();
        await context.Database.MigrateAsync(); 
        await StoreContextSeed.SeedAsync(context, loggerFactory);
    }
    catch (Exception ex)
    {
        var logger = loggerFactory.CreateLogger<Program>();
        logger.LogError(ex, "An error occured during migration");
    }
}



// Configure the HTTP request pipeline.

app.UseMiddleware<ExceptionMiddleware>();
app.UseSwagger();
app.UseSwaggerUI();

app.UseStatusCodePagesWithReExecute("/errors/{0}"); 

app.UseHttpsRedirection();

app.UseRouting();
app.UseStaticFiles();
app.UseCors("CorsPolicy");
app.UseAuthorization();

app.MapControllers();

app.Run();
