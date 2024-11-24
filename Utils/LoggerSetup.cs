using Serilog;

public static class LoggerSetup
{
    public static ILogger ConfigureLogger()
    {
        return new LoggerConfiguration()
            .WriteTo.Console()
            .CreateLogger();
    }
}