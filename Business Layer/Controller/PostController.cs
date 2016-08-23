
using GlobalLogic.BLL.Model;
using GlobalLogic.Helper;
using GlobalLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
namespace GlobalLogic.BLL.Controller
{
    static class PostController
    {
        static ILogger logger = new LogService(typeof(PostController));
        public static List<Post> GetPosts()
        {
            try
            {
                var posts = new List<Post>();
                var uri = ConfigurationManager.AppSettings["postUri"];
                if (uri != null)
                {
                    var webclient = new WebClient();
                    var content = webclient.DownloadData(uri);
                    posts = JsonToObjectCollection.getData<Post>(content);
                }
                return posts;
            }
            catch (WebException ex)
            {
                logger.Error(String.Format("Error in connecting with website. Exception:{0} StackTrace:{1}", ex.Message, ex.StackTrace));
                throw;
            }
        }
    }
}
