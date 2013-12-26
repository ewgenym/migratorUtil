using System.ServiceModel;

namespace migratorUtils.Console
{
    [ServiceContract]
    public interface IMigrationNumberSync
    {
        [OperationContract(IsOneWay = true)]
        void Occupy(string projectId, string number);

        [OperationContract(IsOneWay = true)]
        void Release(string projectId, string number);
    }
}