using migratorUtils.Console.Events;

namespace migratorUtils.Console.Infrastructure
{
    public class EventPublisher : IEventPublisher
    {
        public void Publish<T>(T @event) where T : IEvent
        {
        }
    }
}