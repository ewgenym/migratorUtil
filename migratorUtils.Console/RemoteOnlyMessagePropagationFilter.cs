using System.Diagnostics;
using System.ServiceModel;
using System.ServiceModel.Channels;

namespace migratorUtils.Console
{
    public class RemoteOnlyMessagePropagationFilter : PeerMessagePropagationFilter
    {
        public RemoteOnlyMessagePropagationFilter()
        {
        }

        public override PeerMessagePropagation ShouldMessagePropagate(Message message, PeerMessageOrigination origination)
        {
            var destination = PeerMessagePropagation.LocalAndRemote;
            if (origination == PeerMessageOrigination.Local)
            {
                Trace.WriteLine("Remote-Only Message Propagation Filter Invoked.");
                destination = PeerMessagePropagation.Remote;
            }
            return destination;
        }
    }
}