using GlobalLogic.Helper;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace GlobalLogic.Utilities
{
    public static class JsonToObjectCollection
    {
        static ILogger logger = new LogService(typeof(JsonToObjectCollection));
        /// <summary>
        /// This method shall serialize the JSON into collection of type <T>.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="uri"></param>
        /// <returns></returns>
        public static List<T> getData<T>(string uri)
        {
            var jsonObjects = new List<T>();
            try
            {
                var webclient = new WebClient();
                var content = webclient.DownloadData(uri);

                var serializer = new DataContractJsonSerializer(typeof(List<T>));
                using (var ms = new MemoryStream(content))
                {
                    jsonObjects = (List<T>)serializer.ReadObject(ms);
                }
                return jsonObjects;
            }
            catch (WebException ex)
            {
                logger.Error(String.Format("Error in connecting with website. Exception:{0} StackTrace:{1}", ex.Message, ex.StackTrace));
                throw;
            }
            catch (Exception ex)
            {
                logger.Error(String.Format("Error in fetching the Post List. Exception:{0} StackTrace:{1}", ex.Message, ex.StackTrace));
                throw;
            }
        }
    }
}
