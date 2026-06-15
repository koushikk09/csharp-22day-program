using CareBridge.Api.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// ===================================================================
// REGISTER THE DATABASE CONTEXT
// 'AddDbContext' tells ASP.NET Core: "whenever any piece of code asks
// for a CareBridgeDbContext, create one for them, configured to talk
// to SQL Server using the connection string we just defined in
// appsettings.json (section 3.7)".
//
// This is called DEPENDENCY INJECTION - you saw this pattern on Day 8
// too. We are not creating the database connection ourselves anywhere
// in our controller code; we just ASK for it, and ASP.NET Core hands
// us a ready-to-use one.
// ===================================================================
builder.Services.AddDbContext<CareBridgeDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("CareBridgeDb")));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowAll");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
