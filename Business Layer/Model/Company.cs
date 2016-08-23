using GlobalLogic.Helper;
using System.Runtime.Serialization;

namespace GlobalLogic.BLL.Model
{
    [DataContract]
    public class Company : NotifyPropertyChanged
    {
        #region "- Constructors -"

        public Company()
        {
            this._bs = string.Empty;
            this._catchPhrase = string.Empty;
            this._name = string.Empty;
        }

        #endregion

        #region "- Public properties -"

        private string _name;
        [DataMember(Name = "name")]
        public string Name
        {
            get { return _name; }
            set { _name = value; OnPropertyChanged(); }
        }

        private string _catchPhrase;
        [DataMember(Name = "catchPhrase")]
        public string CatchPhrase
        {
            get { return _catchPhrase; }
            set { _catchPhrase = value; OnPropertyChanged(); }
        }

        private string _bs;
        [DataMember(Name = "bs")]
        public string BS
        {
            get { return _bs; }
            set { _bs = value; OnPropertyChanged(); }
        }
        #endregion
    }
}
