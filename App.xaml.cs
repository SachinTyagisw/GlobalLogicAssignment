using GlobalLogic.Helper;
using GlobalLogic.UI.ViewModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using System.Windows;

namespace GlobalLogic_Assignment
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application,INotifyPropertyChanged
    {
        //public static GlobalProperties GlobalProp = new GlobalProperties();

        private string _statusBarContent = "sachin tyagi";
        public string StatusBarContent
        {
            get
            {
                return _statusBarContent;
            }
            set
            {
                _statusBarContent = value;
                OnPropertyChanged();
            }
        }

        public App()
        {
            //GlobalProp.StatusBarContent = "test";
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            try
            {
                PropertyChangedEventHandler handler = this.PropertyChanged;
                if (handler != null)
                {
                    var e = new PropertyChangedEventArgs(propertyName);
                    handler(this, e);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
