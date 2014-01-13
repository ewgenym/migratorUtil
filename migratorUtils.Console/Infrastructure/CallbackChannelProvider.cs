using System.ServiceModel;

namespace migratorUtils.Console.Infrastructure
{
    public class CallbackChannelProvider : ICallbackChannelProvider
    {
        public T GetCallbackChannel<T>()
        {
            return OperationContext.Current.GetCallbackChannel<T>();
        }
    }
}