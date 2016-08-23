using GlobalLogic.Helper;
using System.Runtime.Serialization;

namespace GlobalLogic.BLL.Model
{
    [DataContract]
    public class User : NotifyPropertyChanged
    {

        #region "- Constructors -"

        public User()
        {
            this._email = string.Empty;
            this._id = -1;
            this._name = string.Empty;
            this._phone = string.Empty;
            this._username = string.Empty;
            this._website = string.Empty;
        }

        #endregion

        #region "- Public properties -"


        private int _id;
        [DataMember(Name = "id")]
        public int ID
        {
            get { return _id; }
            set { _id = value; OnPropertyChanged(); }
        }

        private string _name;
        [DataMember(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private string _username;
        [DataMember(Name = "username")]
        public string UserName
        {
            get { return _username; }
            set { _username = value; OnPropertyChanged(); }
        }

        private string _email;
        [DataMember(Name = "email")]
        public string Email
        {
            get { return _email; }
            set { _email = value; OnPropertyChanged(); }
        }

        private Address _address;
        [DataMember(Name = "address")]
        public Address Address
        {
            get { return _address; }
            set { _address = value; OnPropertyChanged(); }
        }

        private string _phone;
        [DataMember(Name = "phone")]
        public string Phone
        {
            get { return _phone; }
            set { _phone = value; OnPropertyChanged(); }
        }

        private string _website;
        [DataMember(Name = "website")]
        public string WebSite
        {
            get { return _website; }
            set { _website = value; OnPropertyChanged(); }
        }

        private Company _company;
        [DataMember(Name = "company")]
        public Company Company
        {
            get { return _company; }
            set { _company = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
