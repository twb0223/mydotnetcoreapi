using EventBus.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Console
{
    public class OrderGeneratorEvent:IEvent
    {
        public Guid OrderID { get; set; }
    }
}
