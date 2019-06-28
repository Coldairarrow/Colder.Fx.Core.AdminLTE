using NLog;
using NLog.Targets;
using NLog.Config;
using System;

class Example
{
    public class CustomTarget : TargetWithLayout
    {
        public CustomTarget(string name)
        {
            Name = name;
        }
        protected override void Write(LogEventInfo logEvent)
        {
            string msg = Layout.Render(logEvent);
            Console.WriteLine($"自定义日志:{msg}");
        }
    }
    static void Main(string[] args)
    {
        // Step 1. Create configuration object 
        var config = new LoggingConfiguration();

        // Step 2. Create targets
        string layout = @"${date:format=yyyy-MM-dd HH\:mm\:ss}|${level}|${event-properties:item=LogType}|${message} ";
        var consoleTarget = new ColoredConsoleTarget("控制台日志")
        {
            Layout = layout
        };
        config.AddTarget(consoleTarget);
        config.AddRuleForAllLevels(consoleTarget);

        var fileTarget = new FileTarget("控制台日志")
        {
            FileName = $"${{basedir}}/A_logs/{DateTime.Now.ToString("yyyy-MM")}/{DateTime.Now.ToString("yyyy-MM-dd")}.txt",
            Layout = layout
        };
        config.AddTarget(fileTarget);
        config.AddRuleForAllLevels(fileTarget);

        var customTarget = new CustomTarget("自定义")
        {
            Layout = layout
        };
        config.AddTarget(customTarget);
        config.AddRuleForAllLevels(customTarget);

        LogManager.Configuration = config;
        Logger logger = LogManager.GetLogger("Example");

        LogEventInfo logEventInfo = new LogEventInfo(LogLevel.Error, "测试", "测试");
        logEventInfo.Properties["LogType"] = "日志类型";
        logger.Log(logEventInfo);
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