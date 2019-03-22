namespace lab4_cs.Tools.Navigation
{
    internal enum ViewType
    {
        PersonList,
        AddPerson,
        EditPerson
    }

    interface INavigationModel
    {
        void Navigate(ViewType viewType);
    }
}
