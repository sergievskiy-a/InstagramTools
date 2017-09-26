using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace InstagramTools.Common.Helpers
{
    public class TasksMonitor
    {
        public TasksMonitor()
        {
            Tasks = new ConcurrentDictionary<string, CancellationTokenSource>();
        }

        private IDictionary<string, CancellationTokenSource> Tasks;

        public void AddTask(string taskName, CancellationTokenSource cancellationTokenSource)
        {
            if (!Tasks.ContainsKey(taskName))
            {
                Tasks.Add(taskName, cancellationTokenSource);
            }
        }

        public void AddTask(string taskName)
        {
            if (!Tasks.ContainsKey(taskName))
            {
                Tasks.Add(taskName, new CancellationTokenSource());
            }
        }

        public CancellationTokenSource GetTokenSource(string taskName)
        {
            return Tasks[taskName];
        }
    }
}
