using ExerciseApp;
using Microsoft.AspNetCore.Hosting;

CreateHostBuilder(args).Build().Run();


IHostBuilder CreateHostBuilder(string[] args) =>
    Host.CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(webBuilder =>
        {
            webBuilder.UseStartup<Startup>();
        });
