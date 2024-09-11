using Microsoft.Build.Framework;
using Microsoft.Extensions.Logging;
using QPR_Application.Models.Entities;
using System;
using System.CodeDom;
using System.Data.SqlClient;
using System.Threading.Tasks;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace QPR_Application.Util
{
    public class DBLoggerProvider : ILoggerProvider
    {
        //private readonly string _connectionString;
        //private readonly string _connectionString;

        //public DBLoggerProvider(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}
        private readonly QPRContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;
        public DBLoggerProvider(QPRContext dbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DBLogger(_dbContext,_httpContext);
        }

        public void Dispose()
        {
        }
    }
    public class DBLogger : ILogger
    {
        //private readonly string _connectionString;

        //public DBLogger(string connectionString)
        //{
        //    _connectionString = connectionString;
        //}

        private readonly QPRContext _dbContext;
        private readonly IHttpContextAccessor _httpContext;

        public DBLogger(QPRContext dbContext, IHttpContextAccessor httpContext)
        {
            _dbContext = dbContext;
            _httpContext = httpContext;
        }
        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return logLevel >= LogLevel.Information; // Adjust log level as needed
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
        {
            if (!IsEnabled(logLevel)) return;

            var message = formatter(state, exception);

            // Save to database asynchronously
            Task.Run(() => SaveLogToDatabase(logLevel, message, exception));
        }

        private async Task SaveLogToDatabase(LogLevel logLevel, string message, Exception exception)
        {
            var logEntry = new QPRApplicationLogs
            {
                LogLevel = logLevel.ToString(),
                Message = message,
                Exception = exception?.ToString(),
                CreatedDate = DateTime.UtcNow,
                UserId = _httpContext?.HttpContext?.Session.GetString("UserName"),
                ip = _httpContext.HttpContext?.Session?.GetString("ipAddress"),
                RequestPath = _httpContext.HttpContext?.Request.Path
            };

            _dbContext.QPRApplicationLogs.Add(logEntry);
            await _dbContext.SaveChangesAsync();
        }
    }
}
