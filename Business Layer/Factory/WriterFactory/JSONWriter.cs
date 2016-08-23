using GlobalLogic.Helper;
using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Text;

namespace GlobalLogic.BLL.Factory.WriterFactory
{
    public class JSONWriter : IFileWriter
    {
        ILogger logger = new LogService(typeof(JSONWriter));
        public string Generate<T>(T obj)
        {
            try
            {
                var serializer = new DataContractJsonSerializer(typeof(T));
                using (var stream = new MemoryStream())
                {
                    serializer.WriteObject(stream, obj);
                    return Encoding.Default.GetString(stream.ToArray());
                }
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("Error in generating the JSSON content from post object. Exception:{0}, StackTrace{1}", ex.Message, ex.StackTrace));
                throw;
            }
        }
    }
}
