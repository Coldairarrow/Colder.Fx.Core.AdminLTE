using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using System;

namespace Coldairarrow.DataRepository
{
    public class EFCoreSqlLogeerProvider : ILoggerProvider
    {
        public EFCoreSqlLogeerProvider(LoggerHandlerContainer loggerHandlerContainer)
        {
            _loggerHandlerContainer = loggerHandlerContainer;
        }
        private LoggerHandlerContainer _loggerHandlerContainer { get; set; }

        public ILogger CreateLogger(string categoryName)
        {
            return new MyLogger(_loggerHandlerContainer);
        }

        public void Dispose()
        { }

        private class MyLogger : ILogger
        {
            public MyLogger(LoggerHandlerContainer loggerHandlerContainer)
            {
                _loggerHandlerContainer = loggerHandlerContainer;
            }
            private LoggerHandlerContainer _loggerHandlerContainer { get; set; }
            public bool IsEnabled(LogLevel logLevel)
            {
                return true;
            }

            public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
            {
                //只记录SQL执行日志
                if (eventId.Id == RelationalEventId.CommandExecuted.Id)
                {
                    string logContent = formatter(state, exception);
                    _loggerHandlerContainer.HandleSqlLog?.Invoke(logContent);
                }
            }

            public IDisposable BeginScope<TState>(TState state)
            {
                return null;
            }
        }

        public class LoggerHandlerContainer
        {
            public Action<string> HandleSqlLog { get; set; }
        }
    }
}