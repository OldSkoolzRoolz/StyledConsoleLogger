﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Configuration;

namespace KC.Dropins.TextFileLogger;



public static class BuilderExtensions
{
    public static ILoggingBuilder AddTextFileLogger(
        this ILoggingBuilder builder)
        {
            builder.AddConfiguration();
            builder.Services.TryAddEnumerable(
                ServiceDescriptor.Singleton<ILoggerProvider, TextFileLoggerProvider>());

            LoggerProviderOptions.RegisterProviderOptions
                <TextFileLoggerConfiguration, TextFileLoggerProvider>(builder.Services);

            return builder;
        }





    public static ILoggingBuilder AddTextFileLogger(
        this ILoggingBuilder builder,
        Action<TextFileLoggerConfiguration> configure)
        {
            builder.AddTextFileLogger();
            builder.Services.Configure(configure);
            return builder;
        }
}