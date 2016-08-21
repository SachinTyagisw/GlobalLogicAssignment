using GlobalLogic.Helper;
using GlobalLogic.Utilities;
using System;
using System.Text;

namespace GlobalLogic.BLL.Factory.WriterFactory
{
    class PlainWriter : IFileWriter
    {
        ILogger logger = new LogService(typeof(JSONWriter));
        public string Generate<T>(T obj)
        {
            try
            {
                var result = new StringBuilder();
                var propertyDetails = InspectObject.GetPropertyValues(obj);
                foreach (var propertyDetail in propertyDetails)
                {
                    result.AppendFormat("{0}:{1}{2}", propertyDetail.Name, propertyDetail.Value, Environment.NewLine);
                }
                return result.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("Error in generating the Plain text from post object. Exception:{0}, StackTrace{1}", ex.Message, ex.StackTrace));
                throw;
            }
        }

    }
}
