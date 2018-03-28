using EventBus.Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace EventBus.Console
{
    public class UserAddedEventHandlerSendEmail : IEventHandler<UserGeneratorEvent>
    {
        public void Handle(UserGeneratorEvent t)
        {
            System.Console.WriteLine(string.Format("{0}的邮件已发送", t.UserID));
        }
    }

    public class UserAddedEventHandlerSendMessage : IEventHandler<UserGeneratorEvent>
    {
        public void Handle(UserGeneratorEvent tEvent)
        {
            System.Console.WriteLine(string.Format("{0}的短信已发送", tEvent.UserID));
        }
    }

    public class UserAddedEventHandlerSendRedbags : IEventHandler<UserGeneratorEvent>, IEventHandler<OrderGeneratorEvent>
    {
        public void Handle(OrderGeneratorEvent tEvent)
        {
            System.Console.WriteLine(string.Format("{0}的下单红包已发送", tEvent.OrderID));
        }

        public void Handle(UserGeneratorEvent tEvent)
        {
            System.Console.WriteLine(string.Format("{0}的注册红包已发送", tEvent.UserID));
        }
    }
}
