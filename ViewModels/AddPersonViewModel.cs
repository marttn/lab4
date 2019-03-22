using lab4_cs.Models;
using lab4_cs.Tools.Managers;
using System;
using System.Threading.Tasks;
using lab4_cs.Tools;
using System.Windows.Input;
using System.Threading;
using System.Text.RegularExpressions;
using lab4_cs.Exceptions;
using System.Windows;
using lab4_cs.Tools.Navigation;

namespace lab4_cs.ViewModels
{
    internal class AddPersonViewModel
    {
        private ICommand addCommand;
        public Person CurrentPerson { get; }

        
        public AddPersonViewModel()
        {
            CurrentPerson = new Person("", "", "");
            StationManager.CurrentPerson = CurrentPerson;
        }
        public ICommand AddCommand
        {
            get
            {
                return addCommand ?? (addCommand = new RelayCommand<object>(AddImplementation, CanAddExecute));
            }
        }

        private bool CanAddExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(CurrentPerson.Name) &&
                   !String.IsNullOrWhiteSpace(CurrentPerson.LastName) &&
                   !String.IsNullOrWhiteSpace(CurrentPerson.Email);
        }

        private async void AddImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    try
                    {
                        Thread.Sleep(1000);
                        IsDateCorrect();
                    }
                    catch (PersonNotBornYetException exc)
                    {
                        MessageBox.Show($"{exc.Message}: {exc.Birthday}");
                        return false;
                    }
                    catch (PersonTooOldException exc)
                    {
                        MessageBox.Show($"{exc.Message}: {exc.Birthday}");
                        return false;
                    }

                    try
                    {
                        ValidateFullName(CurrentPerson.Name, CurrentPerson.LastName);
                    }
                    catch (InvalidNameException exc)
                    {
                        MessageBox.Show($"{exc.Message} {exc.Name}!");
                        return false;
                    }
                    catch (InvalidLastNameException exc)
                    {
                        MessageBox.Show($"{exc.Message} {exc.LastName}!");
                        return false;
                    }

                    try
                    {
                        ValidateEmail(CurrentPerson.Email);
                    }
                    catch (InvalidEmailAdressException exc)
                    {
                        MessageBox.Show(exc.Message);
                        return false;
                    }
                }
                catch (Exception exc)
                {
                    MessageBox.Show($"something went wrong while adding a new person\nreason: {exc.Message}");
                    return false;
                }
                MessageBox.Show($"Person {CurrentPerson.Name} was successfully added.");
                return true;
            });

            LoaderManager.Instance.HideLoader();

            if (result)
            {
                NavigationManager.Instance.Navigate(ViewType.PersonList);
            }
                

        }

        private void IsDateCorrect()
        {
            if ((DateTime.Now.Year - CurrentPerson.Birthday.Year) > 135)
            {
                throw new PersonTooOldException(CurrentPerson.Birthday);
            }
            else if (CurrentPerson.Birthday > DateTime.Now)
            {
                throw new PersonNotBornYetException(CurrentPerson.Birthday);
            }
        }


        private void ValidateFullName(string name, string lastName)
        {
            Regex regex = new Regex("^[a-zA-Z]+$");
            if (!regex.IsMatch(name))
            {
                throw new InvalidNameException(name);
            }
            if (!regex.IsMatch(lastName))
            {
                throw new InvalidLastNameException(lastName);
            }
        }


        private void ValidateEmail(string email)
        {
            Regex regex = new Regex(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");
            if (!regex.IsMatch(email))
            {
                throw new InvalidEmailAdressException(email);
            }
        }
    }
}
