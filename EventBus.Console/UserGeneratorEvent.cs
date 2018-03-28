using EventBus.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Console
{
    public class UserGeneratorEvent:IEvent
    {
        public Guid UserID { get; set; }
    }
}
