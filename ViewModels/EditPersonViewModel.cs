using lab4_cs.Exceptions;
using lab4_cs.Models;
using lab4_cs.Tools;
using lab4_cs.Tools.Managers;
using lab4_cs.Tools.Navigation;
using System;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace lab4_cs.ViewModels
{
    internal class EditPersonViewModel
    {
        private ICommand editCommand;
        public Person CurrentPerson { get; }

        public EditPersonViewModel()
        {
            CurrentPerson = StationManager.CurrentPerson;
        }

        

        public ICommand EditCommand
        {
            get
            {
                return editCommand ?? (editCommand = new RelayCommand<object>(EditImplementation, CanEditExecute));
            }
        }


        private bool CanEditExecute(object obj)
        {
            return !String.IsNullOrWhiteSpace(CurrentPerson.Name) &&
                  !String.IsNullOrWhiteSpace(CurrentPerson.LastName) &&
                  !String.IsNullOrWhiteSpace(CurrentPerson.Email);
        }

        private async void EditImplementation(object obj)
        {
            LoaderManager.Instance.ShowLoader();
            var result = await Task.Run(() =>
            {
                try
                {
                    try
                    {
                        Thread.Sleep(800);
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
                    MessageBox.Show($"something went wrong while editing a person\nreason: {exc.Message}");
                    return false;
                }
                MessageBox.Show($"Person {CurrentPerson.Name} was successfully updated.");
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
