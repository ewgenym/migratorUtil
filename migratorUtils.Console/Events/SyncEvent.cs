using System.Collections.Generic;

namespace migratorUtils.Console.Events
{
    public class SyncEvent : IEvent
    {
        public IList<Migration> Migrations;
    }
}