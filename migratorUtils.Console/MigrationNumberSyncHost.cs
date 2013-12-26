using System;
using System.Net;
using System.ServiceModel;

namespace migratorUtils.Console
{
    public class MigrationNumberSyncHost
    {
        private ServiceHost _serviceHost;

        public void Start()
        {
            const string port = "4322";
            var serviceUrl = ServiceUrl(port);

            var service = new MigrationNumberSyncService();
            _serviceHost = new ServiceHost(service, new Uri(serviceUrl));
            var binding = new NetTcpBinding(SecurityMode.None);
            _serviceHost.AddServiceEndpoint(typeof (IMigrationNumberSync), binding, serviceUrl);
            _serviceHost.Open();
            System.Console.WriteLine("Service host started...");
        }

        public void Stop()
        {
            if (_serviceHost != null)
            {
                _serviceHost.Close();
                _serviceHost = null;
                System.Console.WriteLine("Service host stoped...");
            }
        }

        // Get service url using IPv4 address and port from config file
        private static string ServiceUrl(string port)
        {
            foreach (var address in Dns.GetHostAddresses(Dns.GetHostName()))
            {
                if (address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    return string.Format("net.tcp://{0}:{1}/MigrationNumberSync", address, port);
                }
            }

            throw new Exception("Unable to determine WCF endpoint.");
        }
    }
}