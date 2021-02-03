using XH.Container;
namespace XH
{
    public interface ICreator
    {
        void Register(IServiceCollection service);
    }
}