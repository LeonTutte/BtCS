using Serilog;
using Serilog.Core;
using Serilog.Events;

namespace BtCS_Library.Modules.Static;

public static class LogModule
{
    private static readonly Logger _logger = new LoggerConfiguration()
        .WriteTo.File($"{ConfigurationModule.GetConfiguration()["Logging"]["LogFolder"]}/log_.txt",
            rollOnFileSizeLimit: true,
            fileSizeLimitBytes: Int32.Parse(ConfigurationModule.GetConfiguration()["Logging"]["MaxFileSize"]),
            rollingInterval: RollingInterval.Day,
            retainedFileCountLimit: Int32.Parse(ConfigurationModule.GetConfiguration()["Logging"]["FileCountLimit"]),
            restrictedToMinimumLevel: LogEventLevel.Verbose)
        .CreateLogger();

    /// <summary>
    ///     Write a Message with Level "Information" to the Log.
    /// </summary>
    /// <param name="message">Your Message for the Log.</param>
    /// <returns></returns>
    public static void WriteInformationMessageToLog(string message) => _logger.Information(message);

    /// <summary>
    ///     Write a Message with Level "Debug" to the Log.
    /// </summary>
    /// <param name="message">Your Message for the Log.</param>
    /// <returns></returns>
    public static void  WriteDebugMessageToLog(string message) => _logger.Debug(message);

    /// <summary>
    ///     Write a Message with Level "Error" to the Log.
    /// </summary>
    /// <param name="message">Your Message for the Log.</param>
    /// <returns></returns>
    public static void WriteErrorMessageToLog(string message) => _logger.Error(message);

    /// <summary>
    ///     Closes the logging Pipeline.
    /// </summary>
    /// <returns></returns>
    public static void CloseLogger() => Log.CloseAndFlush();
}