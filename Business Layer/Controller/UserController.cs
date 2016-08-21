using GlobalLogic.BLL.Model;
using GlobalLogic.Utilities;
using System.Collections.Generic;
using System.Configuration;

namespace GlobalLogic.BLL.Controller
{
    static class UserController
    {
        public static List<User> GetUsers()
        {
            var users = new List<User>();
            var uri = ConfigurationManager.AppSettings["userUri"];
            if (uri != null)
                users = JsonToObjectCollection.getData<User>(uri);
            return users;
        }
    }
}
