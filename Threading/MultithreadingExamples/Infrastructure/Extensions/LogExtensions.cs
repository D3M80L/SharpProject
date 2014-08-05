namespace MultithreadingExamples.Infrastructure.Extensions
{
    public static class LogExtensions
    {
        public static void Info(this ILog log, string message)
        {
            if (log == null)
            {
                return;
            }

            log.Log(message);
        }

        public static void Info(this ILog log, string format, params object[] arguments)
        {
            if (log == null)
            {
                return;
            }

            var message = string.Format(format, arguments);
            log.Log(message);
        }
    }
}
