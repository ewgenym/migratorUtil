namespace migratorUtils.Console.Events
{
    public class ReleaseEvent : IEvent
    {
        public string ProjectId;
        public string Number;
    }
}