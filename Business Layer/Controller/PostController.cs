
using GlobalLogic.BLL.Model;
using GlobalLogic.Utilities;
using System.Collections.Generic;
using System.Configuration;
namespace GlobalLogic.BLL.Controller
{
    static class PostController
    {
        public static List<Post> GetPosts()
        {
            var posts = new List<Post>();
            var uri = ConfigurationManager.AppSettings["postUri"];
            if (uri != null)
                posts = JsonToObjectCollection.getData<Post>(uri);

            return posts;
        }
    }
}
