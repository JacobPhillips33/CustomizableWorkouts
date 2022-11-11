using ExerciseApp;
using ExerciseApp.Repositories;
using Microsoft.AspNetCore.Hosting;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddTransient<IExerciseRepository, ExerciseRepository>();
builder.Services.AddTransient<IWorkoutRepository, WorkoutRepository>();

builder.Services.AddScoped<MySqlConnection>(s =>
{
    MySqlConnection mySqlConn = new MySqlConnection(builder.Configuration.GetConnectionString("exerciseapp"));
    mySqlConn.Open();
    return mySqlConn;
});

builder.Services.AddScoped<IDbConnection>(s =>
{
    IDbConnection conn = new MySqlConnection(builder.Configuration.GetConnectionString("exerciseapp"));
    conn.Open();
    return conn;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();