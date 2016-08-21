
namespace GlobalLogic.BLL.Factory.WriterFactory
{
    public interface IFileWriter
    {
        string Generate<T>(T obj);
    }
}
