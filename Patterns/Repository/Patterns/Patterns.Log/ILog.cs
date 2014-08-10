using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Patterns.Log
{
    public interface ILog
    {
        void Log(LogLevel logLevel, string message, Exception exception);

        void Log(LogLevel logLevel, Action<FormatMessageHandler> messageFormat, Exception exception);

        bool IsEnabled(LogLevel logLevel);
    }
}
