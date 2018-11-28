using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTDD.model.task.delay
{
    public class AsyncDelay : IAsyncDelay
    {
        public Task Delay(TimeSpan timeSpan)
        {
            return Task.Delay(timeSpan);
        }
    }
}
