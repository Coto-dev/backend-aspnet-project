using BackendDev.Data.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<ContextDataBase>(options => options.UseSqlServer(connection));

var app = builder.Build();

using var serviceScope = app.Services.CreateScope();

var context = serviceScope.ServiceProvider.GetService<ContextDataBase>();

// auto migration
/*context?.Database.Migrate();*/


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

/*app.UseEndpoints(endpoints =>
{
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});*/

app.MapControllers();

app.Run();
