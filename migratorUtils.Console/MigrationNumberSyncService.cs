using System.Collections.Generic;
using System.ServiceModel;
using migratorUtils.Console.Events;

namespace migratorUtils.Console
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, UseSynchronizationContext = true)]
    public class MigrationNumberSyncService : IMigrationNumberSync
    {
        private readonly IEventPublisher _eventPublisher;
        private readonly ICallbackChannelProvider _callbackChannelProvider;

        private IMigrationNumberSync CallbackChannel
        {
            get
            {
                return _callbackChannelProvider.GetCallbackChannel<IMigrationNumberSync>();
            }
        }

        public MigrationNumberSyncService(IEventPublisher eventPublisher, ICallbackChannelProvider callbackChannelProvider)
        {
            _eventPublisher = eventPublisher;
            _callbackChannelProvider = callbackChannelProvider;
        }

        public void Occupy(Migration migration)
        {
            _eventPublisher.Publish(new OccupyEvent{ProjectId = migration.ProjectId, Number = migration.Number});
            System.Console.WriteLine("Occupy received: {0} {1}", migration.ProjectId, migration.Number);
        }

        public void Release(Migration migration)
        {
            _eventPublisher.Publish(new ReleaseEvent { ProjectId = migration.ProjectId, Number = migration.Number });

            System.Console.WriteLine("Release received: {0} {1}", migration.ProjectId, migration.Number);
        }

        public void Sync(List<Migration> migrations)
        {
            _eventPublisher.Publish(new SyncEvent {Migrations = migrations});

            System.Console.WriteLine("Sync received");
        }

        public void Join(JoinRequest request)
        {
            _eventPublisher.Publish(new JoinEvent());
            CallbackChannel.Sync(new List<Migration>());

            System.Console.WriteLine("Join received");
        }
    }
}