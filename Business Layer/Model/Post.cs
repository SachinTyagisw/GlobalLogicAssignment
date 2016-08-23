using GlobalLogic.Helper;
using System.Runtime.Serialization;

namespace GlobalLogic.BLL.Model
{
    [DataContract]
    public class Post : NotifyPropertyChanged
    {
        #region "- Constructors -"

        public Post()
        {
            this._userID = -1;
            this._id = -1;
            this._title = string.Empty;
            this._body = string.Empty;
            this._user = null;
        }
        
        #endregion

        #region "- Public properties -"

        private int _userID;
        [DataMember(Name="userId")]
        public int UserID
        {
            get { return _userID; }
            set { _userID = value; OnPropertyChanged(); }
        }

        
        private int _id;
        [DataMember(Name = "id")]
        public int ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private string _title;
        [DataMember(Name = "title")]
        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(); }
        }

        private string _body;
        [DataMember(Name = "body")]
        public string Body
        {
            get { return _body; }
            set { _body = value; OnPropertyChanged(); }
        }

        #endregion

        #region "- Extended Properties -"
        private User _user;
        [DataMember(Name = "user")]
        public User User
        {
            get { return _user; }
            set { _user = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
