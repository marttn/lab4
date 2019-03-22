using lab4_cs.Models;
using lab4_cs.Tools;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using lab4_cs.Tools.Managers;
using System.Linq;
using lab4_cs.Tools.Navigation;
using System.Windows;

namespace lab4_cs.ViewModels
{
    internal class PersonsListViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<Person> persons;
        private Person _selectedItem;
        private string selectedProperty;
        private string query;

        private ICommand deleteCommand;
        private ICommand insertCommand;
        private ICommand updateCommand;
        private ICommand sortCommand;
        private ICommand filterCommand;
        private ICommand resetCommand;
        private ICommand closeCommand;

        public PersonsListViewModel()
        {
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
        }

        public Person SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                _selectedItem = value;
                OnPropertyChanged();
            }
        }
        public string SelectedProperty
        {
            get
            {
                return selectedProperty;
            }
            set
            {
                selectedProperty = value;
                OnPropertyChanged();
            }
        }

        public string Query
        {
            get
            {
                return query;
            }
            set
            {
                query = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Person> Persons
        {
            get { return persons; }
            set
            {
                persons = value;
                OnPropertyChanged();
            }
        }
        

       
        public ICommand DeleteCommand
        {
            get
            {
                return deleteCommand ?? (deleteCommand = new RelayCommand<object>(DeleteSelected, CanModificateTable));
            }
        }

        public ICommand InsertCommand
        {
            get
            {
                return insertCommand ?? (insertCommand = new RelayCommand<object>(InsertImplementation));
            }
        }

        public ICommand UpdateCommand
        {
            get
            {
                return updateCommand ?? (updateCommand = new RelayCommand<object>(UpdateImplementation, CanModificateTable));
            }
        }

        public ICommand SortCommand
        {
            get
            {
                return sortCommand ?? (sortCommand = new RelayCommand<object>(SortImplementation, CanExecute));
            }
        }

        public ICommand FilterCommand
        {
            get
            {
                return filterCommand ?? (filterCommand = new RelayCommand<object>(FilterImplementation, CanExecute));
            }
        }

        public ICommand ResetCommand
        {
            get
            {
                return resetCommand ?? (resetCommand = new RelayCommand<object>(ResetImplementation));
            }
        }
        public ICommand CloseCommand
        {
            get
            {
                return closeCommand ?? (closeCommand = new RelayCommand<object>(CloseImplementation));
            }
        }



        private void DeleteSelected(object o)
        {
            StationManager.CurrentPerson = SelectedItem;
            StationManager.DataStorage.RemovePerson(StationManager.CurrentPerson);
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
            SelectedItem = null;
        }

        private void InsertImplementation(object o)
        {
            NavigationManager.Instance.Navigate(ViewType.AddPerson);
            StationManager.DataStorage.AddPerson(StationManager.CurrentPerson);
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);

        }

        private void UpdateImplementation(object o)
        {

            StationManager.CurrentPerson = SelectedItem;
            NavigationManager.Instance.Navigate(ViewType.EditPerson);
            var idx = Persons.IndexOf(StationManager.CurrentPerson);
            StationManager.DataStorage.UpdatePerson(idx, StationManager.CurrentPerson);
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
            SelectedItem = null;

        }

        private bool CanModificateTable(object o)
        {
            return SelectedItem != null;
        }

        private void SortImplementation(object o)
        {
            if (SelectedProperty.Contains("Name"))
                Persons = new ObservableCollection<Person>(from i in Persons orderby i.Name ascending select i);
            else if (SelectedProperty.Contains("Last name"))
                Persons = new ObservableCollection<Person>(from i in Persons orderby i.LastName ascending select i);
            else if (SelectedProperty.Contains("Birthday"))
                Persons = new ObservableCollection<Person>(from i in Persons orderby i.Birthday ascending select i);
            else if (SelectedProperty.Contains("Email"))
                Persons = new ObservableCollection<Person>(from i in Persons orderby i.Email ascending select i);
            else if (SelectedProperty.Contains("Is adult"))
                Persons = new ObservableCollection<Person>(from i in Persons orderby i.IsAdult ascending select i);
            else if (SelectedProperty.Contains("Is birthday"))
                Persons = new ObservableCollection<Person>(from i in Persons orderby i.IsBirthday ascending select i);
            else if (SelectedProperty.Contains("Sun sign"))
                Persons = new ObservableCollection<Person>(from i in Persons orderby i.SunSign ascending select i);
            else if (SelectedProperty.Contains("Chinese sign"))
                Persons = new ObservableCollection<Person>(from i in Persons orderby i.ChineseSign ascending select i);
        }

        private void FilterImplementation(object o)
        {
            if (SelectedProperty.Contains("Name"))
                Persons = new ObservableCollection<Person>(from i in Persons where i.Name.Contains(Query) select i);
            else if (SelectedProperty.Contains("Last name"))
                Persons = new ObservableCollection<Person>(from i in Persons where i.LastName.Contains(Query) select i);
            else if (SelectedProperty.Contains("Birthday"))
                Persons = new ObservableCollection<Person>(from i in Persons where i.Birthday.ToShortDateString().Contains(Query) select i);
            else if (SelectedProperty.Contains("Email"))
                Persons = new ObservableCollection<Person>(from i in Persons where i.Email.Contains(Query) select i);
            else if (SelectedProperty.Contains("Is adult"))
                Persons = new ObservableCollection<Person>(from i in Persons where i.IsAdult.ToString().Contains(Query) select i);
            else if (SelectedProperty.Contains("Is birthday"))
                Persons = new ObservableCollection<Person>(from i in Persons where i.IsBirthday.ToString().Contains(Query) select i);
            else if (SelectedProperty.Contains("Sun sign"))
                Persons = new ObservableCollection<Person>(from i in Persons where i.SunSign.Contains(Query) select i);
            else if (SelectedProperty.Contains("Chinese sign"))
                Persons = new ObservableCollection<Person>(from i in Persons where i.ChineseSign.Contains(Query) select i);
        }


        private bool CanExecute(object o)
        {
            return SelectedProperty != null;
        }

        private void ResetImplementation(object o)
        {
            Persons = new ObservableCollection<Person>(StationManager.DataStorage.PersonsList);
        }

        private void CloseImplementation(object o)
        {
            StationManager.CloseApp();
        }

        
        

        public event PropertyChangedEventHandler PropertyChanged;


        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
