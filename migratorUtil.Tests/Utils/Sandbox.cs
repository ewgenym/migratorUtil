using System;

namespace migratorUtils.Tests.Utils
{
    public static class Sandbox
    {
        public static void Execute(Action action)
        {
            var setup = new AppDomainSetup
                {
                    ApplicationBase = AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                };

            var appDomain = AppDomain.CreateDomain("Sandbox.Execute", null, setup);
            try
            {
                var worker = new SandboxWorker(action);
                appDomain.DoCallBack(worker.DoWork);
            }
            finally
            {
                AppDomain.Unload(appDomain);
            }
        }
    }

    [Serializable]
    public class SandboxWorker
    {
        private readonly Action _action;

        public SandboxWorker(Action action)
        {
            _action = action;
        }

        public void DoWork()
        {
            _action();
        }
    }
}