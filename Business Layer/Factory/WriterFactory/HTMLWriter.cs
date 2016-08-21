using GlobalLogic.Helper;
using GlobalLogic.Utilities;
using System;
using System.Text;

namespace GlobalLogic.BLL.Factory.WriterFactory
{
    class HTMLWriter : IFileWriter
    {
        ILogger logger = new LogService(typeof(HTMLWriter));
        public string Generate<T>(T obj)
        {
            try
            {
                var result = new StringBuilder();
                result.AppendLine("<!DOCTYPE HTML>");
                result.AppendLine("<html>");
                result.AppendLine("<head>");
                result.AppendLine("<title>HTML representation of post object</title>");
                result.AppendLine("</head>");
                result.AppendLine("<body>");
                result.Append("<table id=\"" + typeof(T).Name + "\">");
                var propertyDetails = InspectObject.GetPropertyValues(obj);
                foreach (var propertyDetail in propertyDetails)
                {
                    result.AppendLine("<tr>");
                    result.AppendFormat("<td>{0}</td>{1}<td>{2}</td>{3}", propertyDetail.Name, Environment.NewLine, propertyDetail.Value, Environment.NewLine);
                    result.AppendLine("</tr>");
                }
                result.AppendLine("</table>");
                result.AppendLine("</body>");
                result.AppendLine("</html>");
                return result.ToString();
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("Error in generating the HTML content from post object. Exception:{0}, StackTrace{1}", ex.Message, ex.StackTrace));
                throw;
            }
        }
    }
}
