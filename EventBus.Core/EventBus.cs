using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Linq;

namespace EventBus.Core
{
    public class EventBus
    {
        /// <summary>
        /// 事件总线对象
        /// </summary>
        private static EventBus eventBus = null;

        /// <summary>
        /// 领域模型事件
        /// </summary>
        private static Dictionary<Type, List<object>> dicEventHandler = new Dictionary<Type, List<object>>();

        private readonly object _syncObject = new object();

        /// <summary>
        /// 单例事件
        /// </summary>
        public static EventBus Instance
        {
            get { return eventBus ?? (eventBus = new EventBus()); }
        }


        public static EventBus InstanceXML()
        {
            if (eventBus == null)
            {
                XElement root = XElement.Load(System.IO.Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EventBus.xml"));

                foreach (var evt in root.Elements("Event"))
                {
                    List<object> handlers = new List<object>();

                    Type publishEventType = Type.GetType(evt.Element("PublishEvent").Value);
                    foreach (var subscritedEvt in evt.Elements("SubscribedEvents"))
                        foreach (var concreteEvt in subscritedEvt.Elements("SubscribedEvent"))
                            handlers.Add(Type.GetType(concreteEvt.Value));

                    dicEventHandler[publishEventType] = handlers;
                }
                eventBus = new EventBus();
            }
            return eventBus;
        }

        private readonly Func<object, object, bool> eventHandlerEquals = (o1, o2) =>
        {
            var o1Type = o1.GetType();
            var o2Type = o2.GetType();
            return o1Type == o2Type;
        };

        public void Subscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IEvent
        {
            //同步锁
            lock (_syncObject)
            {
                //获取领域模型的类型
                var eventType = typeof(TEvent);
                //如果此领域类型在事件总线中已注册过
                if (dicEventHandler.ContainsKey(eventType))
                {
                    var handlers = dicEventHandler[eventType];
                    if (handlers != null)
                    {
                        handlers.Add(eventHandler);
                    }
                    else
                    {
                        handlers = new List<object>
                        {
                            eventHandler
                        };
                    }
                }
                else
                {
                    dicEventHandler.Add(eventType, new List<object> { eventHandler });
                }
            }
        }
        /// <summary>
        /// 订阅事件实体
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subTypeList"></param>
        public void Subscribe<TEvent>(Action<TEvent> eventHandlerFunc)
            where TEvent : IEvent
        {
            Subscribe(new ActionDelegatedEventHandler<TEvent>(eventHandlerFunc));
        }
        public void Subscribe<TEvent>(IEnumerable<IEventHandler<TEvent>> eventHandlers)
            where TEvent : IEvent
        {
            foreach (var eventHandler in eventHandlers)
            {
                Subscribe(eventHandler);
            }
        }



        #region 发布事件

        public void Publish<TEvent>(TEvent tEvent, Action<TEvent, bool, Exception> callback) where TEvent : IEvent
        {
            var eventType = typeof(TEvent);
            if (dicEventHandler.ContainsKey(eventType) && dicEventHandler[eventType] != null &&
                dicEventHandler[eventType].Count > 0)
            {
                var handlers = dicEventHandler[eventType];
                try
                {
                    foreach (var handler in handlers)
                    {
                        var eventHandler = handler as IEventHandler<TEvent>;
                        eventHandler.Handle(tEvent);
                        callback(tEvent, true, null);
                    }
                }
                catch (Exception ex)
                {
                    callback(tEvent, false, ex);
                }
            }
            else
            {
                callback(tEvent, false, null);
            }
        }

        #endregion

        #region 取消订阅
        /// <summary>
        /// 取消订阅事件
        /// </summary>
        /// <param name="type"></param>
        /// <param name="subType"></param>
        public void Unsubscribe<TEvent>(IEventHandler<TEvent> eventHandler) where TEvent : IEvent
        {
            lock (_syncObject)
            {
                var eventType = typeof(TEvent);
                if (dicEventHandler.ContainsKey(eventType))
                {
                    var handlers = dicEventHandler[eventType];
                    if (handlers != null
                        && handlers.Exists(deh => eventHandlerEquals(deh, eventHandler)))
                    {
                        var handlerToRemove = handlers.First(deh => eventHandlerEquals(deh, eventHandler));
                        handlers.Remove(handlerToRemove);
                    }
                }
            }
        }
        #endregion

    }
}
