using GlobalLogic.BLL.Model;
using GlobalLogic.Helper;
using GlobalLogic.Utilities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;

namespace GlobalLogic.BLL.Controller
{
    static class UserController
    {
        static ILogger logger = new LogService(typeof(UserController));
        public static List<User> GetUsers()
        {
            try
            {
                var users = new List<User>();
                var uri = ConfigurationManager.AppSettings["userUri"];
                if (uri != null)
                {
                    var webclient = new WebClient();
                    var content = webclient.DownloadData(uri);
                    users = JsonToObjectCollection.getData<User>(content);
                }
                return users;
            }
            catch (WebException ex)
            {
                logger.Error(String.Format("Error in connecting with website. Exception:{0} StackTrace:{1}", ex.Message, ex.StackTrace));
                throw;
            }
        }
    }
}
