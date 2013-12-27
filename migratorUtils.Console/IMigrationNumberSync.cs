using System.Collections.Generic;
using System.ServiceModel;

namespace migratorUtils.Console
{
    [ServiceContract(Namespace = "http://scalepoint.com/peerchannel", CallbackContract = typeof(IMigrationNumberSync))]
    public interface IMigrationNumberSync
    {
        [OperationContract(IsOneWay = true)]
        void Occupy(Migration migration);

        [OperationContract(IsOneWay = true)]
        void Release(Migration migration);

        [OperationContract(IsOneWay = true)]
        void Sync(List<Migration> migrations);

        [OperationContract(IsOneWay = true)]
        void Join(JoinRequest request);
    }

    [MessageContract]
    public class JoinRequest
    {
        [PeerHopCount]
        public int HopCount;
    }

    [MessageContract]
    public class Migration
    {
        [MessageBodyMember]
        public string ProjectId;

        [MessageBodyMember]
        public string Number;
    }
}