using System;
using System.Net;
using System.ServiceModel;
using System.ServiceModel.PeerResolvers;
using migratorUtils.Console.Infrastructure;

namespace migratorUtils.Console
{
    public class MigrationNumberSyncPeer
    {
        private readonly IMigrationNumberSync _migrationNumberSync;

        public MigrationNumberSyncPeer(IMigrationNumberSync migrationNumberSync)
        {
            _migrationNumberSync = migrationNumberSync;
        }

        private const bool _remoteOnlyMessages = false;
        public IMigrationNumberSync Channel { get; private set; }

        public void Start(int port)
        {
            var url = ServiceUrl();

            var context = new InstanceContext(_migrationNumberSync);
            var binding = new NetPeerTcpBinding();
            binding.Security.Mode = SecurityMode.None;
            binding.Resolver.Mode = PeerResolverMode.Pnrp;
            binding.Port = port;
            binding.Name = url + "@" + port;

            var address = new EndpointAddress(url);
            var channelFactory = new DuplexChannelFactory<IMigrationNumberSync>(context, binding, address);

            var proxy = channelFactory.CreateChannel();

            if (_remoteOnlyMessages)
            {
                SetupRemoteOnlyPropogationFilter(((IClientChannel)proxy));
            }

            ((IClientChannel)proxy).Open();

            Channel = proxy;
        }

        private static void SetupRemoteOnlyPropogationFilter(IClientChannel channel)
        {
            var remoteOnlyFilter = new RemoteOnlyMessagePropagationFilter();
            var peerNode = channel.GetProperty<PeerNode>();
            peerNode.MessagePropagationFilter = remoteOnlyFilter;
        }

        public void Stop()
        {
            if (Channel != null)
            {
                ((IClientChannel)Channel).Close();
            }
        }

        private string ServiceUrl()
        {
            foreach (var address in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return string.Format("net.p2p://{0}/MigrationNumberSync", address);
                }
            }

            throw new Exception("Unable to determine WCF endpoint.");
        }
    }
}