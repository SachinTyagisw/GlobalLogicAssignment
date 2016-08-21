
namespace GlobalLogic.BLL.Factory.WriterFactory
{
    public class WriterFactory
    {
        public static IFileWriter getWriter(WriterType writerType)
        {
            IFileWriter _objWriter;
            switch (writerType)
            {
                case WriterType.HTML:
                    _objWriter = new HTMLWriter();
                    break;

                case WriterType.JSON:
                    _objWriter = new JSONWriter();
                    break;

                case WriterType.Plain:
                    _objWriter = new PlainWriter();
                    break;

                default:
                    _objWriter = new PlainWriter();
                    break;
            }
            return _objWriter;
        }
    }

    public enum WriterType { Plain, JSON,HTML }
}
