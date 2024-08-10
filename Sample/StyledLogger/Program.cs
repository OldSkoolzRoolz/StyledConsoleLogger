﻿using KC.Dropins.TextFileLogger;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;


namespace KC.Apps.Logging;



internal class Program
{
    public static void Main(string[] args)
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, eventArgs) =>
            {
                Console.WriteLine("Unhandled exception in main:");
                Console.WriteLine(eventArgs.ExceptionObject.ToString());
            };

            AppDomain.CurrentDomain.FirstChanceException += (sender, eventArgs) =>
            {
                Console.WriteLine("First Chance exception");
                Console.WriteLine(eventArgs.Exception.Message);
            };

            var builder = Host.CreateApplicationBuilder(args);
            builder.Environment.ApplicationName = "StyledLoggers";
            try
            {
                builder.Logging.ClearProviders();
                builder.Logging.AddTextFileLogger(
                    configuration =>
                    {
                        // just a little flare to make the entries stand out
                        // can be text chars as below or string variable
                        configuration.EntryPrefix = "~~<[";
                        configuration.EntrySuffix = "]>~~";

                        // Timespan options
                        configuration.TimestampFormat = "HH:mm:ss";
                        configuration.UseUtcTime = false;

                        // Scope info
                        configuration.IncludeScopes = false;

                        // Send all log entries to one file
                        // or create individual logs for each category
                        configuration.UseSingleLogFile = true;
                        // TBD
                        configuration.LogRotationPolicy = LogRotationPolicy.Hourly;
                    });
            }
            catch (Exception e)
            {
                Console.WriteLine($"error adding providers---{e.Message}");
            }

            using var host = builder.Build();
            try
            {
                var loggerFactory = host.Services.GetRequiredService<ILoggerFactory>();
                var logger = loggerFactory.CreateLogger<Program>();
                var logger2 = loggerFactory.CreateLogger("AnotherCategory");
                var logger3 = loggerFactory.CreateLogger("SeperateLogFile");
                logger2.LogInformation("This is for logger2 testing. ssame LogFilename");
                logger3.LogInformation("Seperate log file testing");
                logger.LogTrace("Trace Test Message");
                logger.LogCritical("Critical Error Test message.");
                logger.LogError("Error Test Message");
                logger.LogDebug("Debug Test message");
                logger.LogWarning("Warning Test Message");
                logger.LogInformation("Hello World!");

                // Logger message attr - defined as an extension method
                logger.ApplicationInfo("Basic Compiled messages");

                //Logger message - passing in ILogger as param
                LoggerMessages.ApplicationWarning(logger, "There was an error in the module");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                Console.WriteLine("Application ending fall-through");
            }
        }
}