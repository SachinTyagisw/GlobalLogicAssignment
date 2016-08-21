using GlobalLogic.Dialog.ViewModel;
using System.Windows.Controls;

namespace GlobalLogic.Dialog.View
{
    /// <summary>
    /// Interaction logic for PostDetailView.xaml
    /// </summary>
    public partial class PostDetailView : UserControl
    {
        public PostDetailView()
        {
            InitializeComponent();
            this.DataContext = new PostDetailVM();
        }
    }
}
