using lab4_cs.Tools.DataStorage;
using lab4_cs.Tools.Managers;
using lab4_cs.Tools.Navigation;
using lab4_cs.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace lab4_cs
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, IContentOwner
    {
        public ContentControl ContentControl
        {
            get { return _contentControl; }
        }

        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainWindowViewModel();
            StationManager.Initialize(new SerializedDataStorage());
            NavigationManager.Instance.Initialize(new InitializationNavigationModel(this));
            NavigationManager.Instance.Navigate(ViewType.PersonList);
        }


    }
}
