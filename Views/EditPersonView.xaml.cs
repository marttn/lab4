using lab4_cs.Tools.Navigation;
using lab4_cs.ViewModels;
using System.Windows.Controls;

namespace lab4_cs.Views
{
    /// <summary>
    /// Логика взаимодействия для EditPersonView.xaml
    /// </summary>
    public partial class EditPersonView : UserControl, INavigatable
    {
        public EditPersonView()
        {
            InitializeComponent();
            DataContext = new EditPersonViewModel();
        }
    }
}
