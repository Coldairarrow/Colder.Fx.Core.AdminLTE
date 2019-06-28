using NLog;
using NLog.Targets;
using NLog.Config;
using System;

class Example
{
    public class CustomTarget : TargetWithLayout
    {
        protected override void Write(LogEventInfo logEvent)
        {
            string logMessage = this.Layout.Render(logEvent);

            Console.WriteLine(logMessage);
        }
    }
    static void Main(string[] args)
    {
        // Step 1. Create configuration object 
        var config = new LoggingConfiguration();

        // Step 2. Create targets
        var consoleTarget = new ColoredConsoleTarget("控制台日志")
        {
            Layout = @"${date:format=HH\:mm\:ss} ${level} ${message} ${exception}"
        };
        config.AddTarget(consoleTarget);

        var fileTarget = new FileTarget("文件日志")
        {
            FileName = $"${{basedir}}/A_logs/{DateTime.Now.ToString("yyyy-MM-dd")}.txt",
            Layout = "${longdate} ${level} ${message}  ${exception}"
        };
        config.AddTarget(fileTarget);

        // Step 3. Define rules
        //config.AddRuleForAllLevels
        config.AddRuleForOneLevel(LogLevel.Error, fileTarget); // only errors to file
        config.AddRuleForAllLevels(consoleTarget); // all to console

        // Step 4. Activate the configuration
        LogManager.Configuration = config;

        // Example usage
        Logger logger = LogManager.GetLogger("Example");
        logger.Trace("trace log message");
        logger.Debug("debug log message");
        logger.Info("info log message");
        logger.Warn("warn log message");
        logger.Error("error log message");
        logger.Fatal("fatal log message");
        //Example of logging exceptions
        try
        {
            throw new Exception("1111111111111");
        }
        catch (Exception ex)
        {
            logger.Error(ex, "ow noos!");
            //throw;
        }
    }
}