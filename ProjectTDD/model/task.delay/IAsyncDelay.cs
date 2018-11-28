using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ProjectTDD.model.task.delay
{
    public interface IAsyncDelay
    {
        Task Delay(TimeSpan timeSpan);
    }
}
