using System.Net.PeerToPeer;

namespace migratorUtils.Console
{
    public class MigrationNumberSyncPeer
    {
        private readonly string _id;
        private PeerNameRegistration _peerNameRegistration;
        private readonly MigrationNumberSyncHost _host;

        public MigrationNumberSyncPeer(string id)
        {
            _id = id;
            _host = new MigrationNumberSyncHost();
        }

        //public IMigrationNumberSync Channel { get; private set; }

        public void Start()
        {
            RegisterPnrpNode();
            _host.Start();
        }

        public void Stop()
        {
            _host.Stop();
            UnregisterPnrpNode();
        }

        private void RegisterPnrpNode()
        {
            var peerName = new PeerName(_id, PeerNameType.Unsecured);

            _peerNameRegistration = new PeerNameRegistration(peerName, 4321)
                {
                    Comment = "Migration number sync",
                    UseAutoEndPointSelection = true
                };
            _peerNameRegistration.Start();

            System.Console.WriteLine("Registration of peer '{0}' completed.", peerName);
        }

        private void UnregisterPnrpNode()
        {
            if (_peerNameRegistration != null)
            {
                var peerName = _peerNameRegistration.PeerName;
                _peerNameRegistration.Stop();
                _peerNameRegistration = null;

                System.Console.WriteLine("Registration of peer '{0}' stoped.", peerName);
            }
        }
    }
}