using lab4_cs.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4_cs.Tools.DataStorage
{
    internal interface IDataStorage
    {
        
        void AddPerson(Person person);
        void RemovePerson(Person person);
        void UpdatePerson(int index, Person person);

        List<Person> PersonsList { get;  }
    }
}
