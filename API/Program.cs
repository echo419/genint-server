using API.Services;
using Core;
using ImageResizer.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Persistence;
using System.Configuration;


var builder = WebApplication.CreateBuilder(args);



builder.Logging.AddLog4Net();


builder.Services.AddSignalR();
builder.Services.AddCors(options =>
{
    options.AddPolicy("CORSPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed((hosts) => true));
});


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(options =>
{
});


builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddDbContext<AppDbContext>(options =>
                options.UseSqlServer("AppDbConnection"));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=Asset}/{action=Get}/{subdir}/{fileName}");

app.UseCors("CORSPolicy");
app.UseRouting();
app.UseAuthorization();

//app.MapControllerRoute(name: "default", pattern: "{controller=Asset}/{action=Get}/{subdir}/{fileName}");

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    

});



//config.Routes.MapHttpRoute(
//    name: "GetDefaultControllerAndAction",
//    routeTemplate: "{subdir}/{filename}",
//    new { controller = "Asset", action = "Get" });

app.UseHttpsRedirection();
app.MapControllers();
app.Run();
