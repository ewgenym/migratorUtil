using System.Collections.Generic;
using System.ServiceModel;

namespace migratorUtils.Console
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MigrationNumberSyncService : IMigrationNumberSync
    {
        public void Occupy(Migration migration)
        {
            System.Console.WriteLine("Received occupy: {0} {1}", migration.ProjectId, migration.Number);
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