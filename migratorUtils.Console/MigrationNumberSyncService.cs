using System;
using System.Collections.Generic;
using System.ServiceModel;
using System.Threading;
using migratorUtils.Console.Events;

namespace migratorUtils.Console
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MigrationNumberSyncService : IMigrationNumberSync
    {
        private readonly IEventPublisher _eventPublisher;

        public MigrationNumberSyncService(IEventPublisher eventPublisher)
        {
            _eventPublisher = eventPublisher;
        }

        public void Occupy(Migration migration)
        {
            System.Console.Out.WriteLine("Occupy on AppDomain.CurrentDomain.FriendlyName = {0}", AppDomain.CurrentDomain.FriendlyName);
            System.Console.WriteLine("Received occupy: {0} {1}. Thread {2}", migration.ProjectId, migration.Number, Thread.CurrentThread.ManagedThreadId);
            _eventPublisher.Publish(new OccupyEvent {ProjectId = migration.ProjectId, Number = migration.Number});
        }

        public void Release(Migration migration)
        {
            System.Console.WriteLine("Received release: {0} {1}", migration.ProjectId, migration.Number);
        }

        public void Sync(List<Migration> migrations)
        {
        }

        public void Join(JoinRequest request)
        {
            System.Console.WriteLine("Received join");
        }
    }
}