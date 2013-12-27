using System.ServiceModel;

namespace migratorUtils.Console
{
    [ServiceContract(Namespace = "http://scalepoint.com/peerchannel", CallbackContract = typeof(IMigrationNumberSync))]
    public interface IMigrationNumberSync
    {
        [OperationContract(IsOneWay = true)]
        void Occupy(string projectId, string number);

        [OperationContract(IsOneWay = true)]
        void Release(string projectId, string number);
    }
}