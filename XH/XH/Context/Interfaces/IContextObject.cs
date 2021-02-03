namespace XH
{
    public interface IContextObject
    {
        IService GetService<IService>() where IService : class;
        IService GetService<IService>(string object_name) where IService : class;
        void ScopedEnd();
    }
}