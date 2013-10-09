using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xunit;

namespace PhotoBuddy.Tests
{
    public class EventArgsTest
    {
        [Fact]
        public void HoldReferenceToEventData()
        {
            var eventArgs = new EventArgs<string>("Hello");
            Assert.Equal("Hello", eventArgs.Data);
        }
    }
}