using System.Collections.Generic;
using System.Linq;
using System.Text;
using MultithreadingExamples.Infrastructure;

namespace MultithreadingExamples.Examples.Tasks
{
    public abstract class TasksExampleBase : ExampleBase
    {
        public const string InTaskState = "InTaskState";
        public const string ThreadId = "ThreadId={0}";
    }
}
