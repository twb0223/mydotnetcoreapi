namespace EventBus.Core
{
    public interface IEventHandler<TEvent> where TEvent:IEvent
    {
        void Handle(TEvent t);
    }
}
