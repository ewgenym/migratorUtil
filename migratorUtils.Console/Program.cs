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
            System.Console.WriteLine("Occupy command");
        }

        [Action]
        public static void Release([Required] string projectId, [Required] string number)
        {
            System.Console.WriteLine("Release command");
        }

        [Action]
        public static void Start()
        {
            var id = string.Format("{0}_{1}", Environment.MachineName, Guid.NewGuid());
            _peer = new MigrationNumberSyncPeer(id);
            _peer.Start();
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
