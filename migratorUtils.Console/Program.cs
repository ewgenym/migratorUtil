using System;
using System.Threading;
using NConsoler;
using migratorUtils.Console.Infrastructure;

namespace migratorUtils.Console
{
    class Program
    {
        private static MigrationNumberSyncPeer _peer;

        static void Main(string[] args)
        {
            while(true)
            {
                System.Console.Write(":");
                var cmds = System.Console.ReadLine();
                Consolery.Run(typeof(Program), cmds.Split(' '));
            }
        }

        [Action]
        public static void Occupy([Required]string projectId, [Required]string number)
        {
            CheckPeerStarted();
            _peer.Channel.Occupy(new Migration {ProjectId = projectId, Number = number});
        }

        [Action]
        public static void Release([Required] string projectId, [Required] string number)
        {
            CheckPeerStarted();
            _peer.Channel.Release(new Migration { ProjectId = projectId, Number = number });
        }

        [Action]
        public static void Start([Required]int port)
        {
            System.Console.WriteLine("Thread: {0}", Thread.CurrentThread.ManagedThreadId);
            if (_peer != null)
            {
                System.Console.WriteLine("Peer is already active.");
            }

            _peer = new MigrationNumberSyncPeer(new MigrationNumberSyncService(new EventPublisher()));
            _peer.Start(port);
        }

        [Action]
        public static void Join()
        {
            CheckPeerStarted();
            _peer.Channel.Join(new JoinRequest {HopCount = 1});
        }

        [Action]
        public static void Stop()
        {
            CheckPeerStarted();
            _peer.Stop();
            _peer = null;
        }

        [Action]
        public static void Exit()
        {
            if (_peer != null)
            {
                _peer.Stop();
            }

            Environment.Exit(0);
        }

        private static void CheckPeerStarted()
        {
            if (_peer == null)
            {
                System.Console.WriteLine("Peer is not started yet.");
            }
        }
    }
}
