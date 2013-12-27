using System;
using NConsoler;

namespace migratorUtils.Console
{
    class Program
    {
        private static MigrationNumberSyncPeer _peer;

        static void Main(string[] args)
        {
            while(true)
            {
                var cmds = System.Console.ReadLine();
                Consolery.Run(typeof(Program), cmds.Split(' '));
            }
        }

        [Action]
        public static void Occupy([Required]string projectId, [Required]string number)
        {
            _peer.Channel.Occupy(new Migration {ProjectId = projectId, Number = number});
            System.Console.WriteLine("Occupy command compledted");
        }

        [Action]
        public static void Release([Required] string projectId, [Required] string number)
        {
            _peer.Channel.Release(new Migration { ProjectId = projectId, Number = number });
            System.Console.WriteLine("Release command compledted");
        }

        [Action]
        public static void Start([Required]int port)
        {
            _peer = new MigrationNumberSyncPeer();
            _peer.Start(port);
        }

        [Action]
        public static void Join()
        {
            _peer.Channel.Join(new JoinRequest{HopCount = 1});
        }

        [Action]
        public static void Stop()
        {
            if (_peer != null)
            {
                _peer.Stop();
            }
        }

        [Action]
        public static void Exit()
        {
            Environment.Exit(0);
        }
    }
}
