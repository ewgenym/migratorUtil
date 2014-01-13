namespace migratorUtils.Console
{
    public interface ICallbackChannelProvider
    {
        T GetCallbackChannel<T>();
    }
}