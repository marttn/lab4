using lab4_cs.Tools.Navigation;
using lab4_cs.ViewModels;
using System.Windows.Controls;

namespace lab4_cs.Views
{
    /// <summary>
    /// Логика взаимодействия для AddPersonView.xaml
    /// </summary>
    public partial class AddPersonView : UserControl, INavigatable
    {
        public AddPersonView()
        {
            InitializeComponent();
            DataContext = new AddPersonViewModel(); 
        }
    }
}
