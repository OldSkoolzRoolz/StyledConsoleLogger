using System.ComponentModel.DataAnnotations;


namespace KC.Dropins.TextFileLogger;



public class TextFileLoggerConfiguration
{
    public bool UseUtcTime { get; set; }
    public bool UseSingleLogFile { get; set; }
    [Required] public string CategoryName { get; set; }

    public LogRotationPolicy LogRotationPolicy { get; set; }
    public string? TimestampFormat { get; set; }
    public string? EntryPrefix { get; set; }
    public string? EntrySuffix { get; set; }
    public bool IncludeScopes { get; set; }
}




public enum LogRotationPolicy
{
    Hourly = 0
    , Daily = 1
    , Weekly = 2
}