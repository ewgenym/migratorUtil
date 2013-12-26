using System.ServiceModel;

namespace migratorUtils.Console
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class MigrationNumberSyncService : IMigrationNumberSync
    {
        public void Occupy(string projectId, string number)
        {
            System.Console.WriteLine("Occupy migration: {0}, project {1}", number, projectId);
        }

        public void Release(string projectId, string number)
        {
            System.Console.WriteLine("Release migration: {0}, project {1}", number, projectId);
        }
    }
}