using GlobalLogic.Helper;
using System.Runtime.Serialization;

namespace GlobalLogic.BLL.Model
{
    [DataContract]
    class Address : NotifyPropertyChanged
    {
        #region "- Constructors -"

        public Address()
        {
            this._city = string.Empty;
            this._street = string.Empty;
            this._suite = string.Empty;
            this._zipcode = string.Empty;
        }

        #endregion

        #region "- Public properties -"

        private string _street;
        [DataMember(Name = "street")]
        public string Street
        {
            get { return _street; }
            set { _street = value; OnPropertyChanged(); }
        }


        private string _suite;
        [DataMember(Name = "suite")]
        public string Suite
        {
            get { return _suite; }
            set { _suite = value; OnPropertyChanged(); }
        }

        private string _city;
        [DataMember(Name = "city")]
        public string City
        {
            get { return _city; }
            set { _city = value; OnPropertyChanged(); }
        }

        private string _zipcode;
        [DataMember(Name = "zipcode")]
        public string Zipcode
        {
            get { return _zipcode; }
            set { _zipcode = value; OnPropertyChanged(); }
        }

        #endregion
    }
}
