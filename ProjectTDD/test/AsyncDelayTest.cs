using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using System.Threading.Tasks;

namespace ProjectTDD.test
{
    public class AsyncDelayTest
    {
        model.task.delay.IAsyncDelay sut;

        public AsyncDelayTest()
        {
            sut = new model.task.delay.AsyncDelay();
        }

        [Fact]
        public async void Delay_Input5Milliseconds_SmokeTest()
        {
            await sut.Delay(TimeSpan.FromMilliseconds(5));
        }
    }
}
