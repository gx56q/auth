﻿using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using PhotosApp.Data;
using Serilog;
using Serilog.Events;
using Serilog.Sinks.SystemConsole.Themes;

namespace PhotosApp
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
                .MinimumLevel.Override("System", LogEventLevel.Warning)
                .MinimumLevel.Override("Microsoft.AspNetCore.Authentication", LogEventLevel.Information)
                .Enrich.FromLogContext()
                .WriteTo.Console(
                    outputTemplate:
                    "[{Timestamp:HH:mm:ss} {Level}] {SourceContext}{NewLine}{Message:lj}{NewLine}{Exception}{NewLine}",
                    theme: AnsiConsoleTheme.Literate)
                .CreateLogger();

            try
            {
                Log.Information("Creating web host builder");
                var hostBuilder = CreateHostBuilder(args);
                Log.Information("Building web host");
                var host = hostBuilder.Build();
                Log.Information("Preparing data");
                host.PrepareData();
                Log.Information("Running web host");
                host.Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        private static IHostBuilder CreateHostBuilder(string[] args)
        {
            return Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    // NOTE: Жесткий способ настройки, который сработает в 100% различных IDE.
                    // Для продакшена следует использовать аргументы командной строки,
                    // переменные окружения, файлы конфигурации
                    webBuilder.UseUrls("https://localhost:5001;http://localhost:5000");
                    webBuilder.UseEnvironment("Development");

                    webBuilder.UseStartup<Startup>();

                    webBuilder.UseSerilog();
                });
        }
    }
}