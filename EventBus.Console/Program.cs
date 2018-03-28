using System;

namespace EventBus.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var sendEmailHandler = new UserAddedEventHandlerSendEmail();
            var sendMessageHandler = new UserAddedEventHandlerSendMessage();
            var sendRedbagsHandler = new UserAddedEventHandlerSendRedbags();

            Core.EventBus.Instance.Subscribe(sendEmailHandler);
            Core.EventBus.Instance.Subscribe(sendMessageHandler);
            Core.EventBus.Instance.Subscribe<OrderGeneratorEvent>(sendRedbagsHandler);


            var userGeneratorEvent = new UserGeneratorEvent { UserID = Guid.NewGuid() };
            System.Console.WriteLine("{0}注册成功", userGeneratorEvent.UserID);


            Core.EventBus.Instance.Publish(userGeneratorEvent, CallBack);

            var orderGeneratorEvent = new OrderGeneratorEvent { OrderID = Guid.NewGuid() };

            System.Console.WriteLine("{0}下单成功", orderGeneratorEvent.OrderID);

            Core.EventBus.Instance.Publish(orderGeneratorEvent, CallBack);

            System.Console.ReadKey();
        }

        private static void CallBack(OrderGeneratorEvent orderGeneratorEvent, bool result, Exception ex)
        {
            System.Console.WriteLine("用户下单订阅事件执行成功");
        }

        public static void CallBack(UserGeneratorEvent userGenerator, bool result, Exception ex)
        {
            System.Console.WriteLine("用户注册订阅事件执行成功");
        }

    }
}
