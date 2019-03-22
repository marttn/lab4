using System;
using PersonListView = lab4_cs.Views.PersonListView;
using AddPersonView = lab4_cs.Views.AddPersonView;
using EditPersonView = lab4_cs.Views.EditPersonView;
namespace lab4_cs.Tools.Navigation
{
    internal class InitializationNavigationModel : BaseNavigationModel
    {
        public InitializationNavigationModel(IContentOwner contentOwner) : base(contentOwner)
        {

        }
        

        protected override void InitializeView(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.PersonList:
                    ViewsDictionary.Add(viewType, new PersonListView());
                    break;
                case ViewType.AddPerson:
                    ViewsDictionary.Add(viewType, new AddPersonView());
                    break;
                case ViewType.EditPerson:
                    ViewsDictionary.Add(viewType, new EditPersonView());
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(viewType), viewType, null);
            }
        }
    }
}
