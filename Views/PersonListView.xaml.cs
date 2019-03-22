using lab4_cs.ViewModels;
using lab4_cs.Tools.Navigation;
using System.Windows.Controls;

namespace lab4_cs.Views
{
    /// <summary>
    /// Логика взаимодействия для PersonListView.xaml
    /// </summary>
    public partial class PersonListView : UserControl, INavigatable
    {
        public PersonListView()
        {

            InitializeComponent();
            DataContext = new PersonsListViewModel();

        }
    }
}
