using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows;
using lab4_cs.Models;
using lab4_cs.Tools.Managers;

namespace lab4_cs.Tools.DataStorage
{
    internal class SerializedDataStorage : IDataStorage
    {
        private List<Person> persons;

        internal SerializedDataStorage()
        {
            try
            {
                persons = SerializationManager.Deserialize<List<Person>>(FileFolderHelper.StorageFilePath);
            }
            catch (FileNotFoundException)
            {
                persons = new List<Person>()
                {
                new Person("mary", "gg", "t@domain.com"),
                new Person("anton", "h", new DateTime(2000, 4, 10)),
                new Person("vlad", "rtyy", "aaa@domain.com"),
                new Person("marina", "gff", new DateTime(2015, 7, 31)),
                new Person("abby", "re", "zzz@domain.com", new DateTime(2001, 3, 14)),
                new Person("josh", "rhb", "453@domain.com"),
                new Person("joshua", "frank", "art@domain.com", new DateTime(1988, 2, 10)),
                new Person("john", "mklk", "f@domain.com"),
                new Person("dfmary", "eeeeee", "d.gfd@domain.com", new DateTime(1965, 3, 12)),
                new Person("rtr", "fg", "ttd@domain.com"),
                new Person("johny", "z", "tteree@domain.com", new DateTime(2016, 2, 29)),
                new Person("kkhggd", "rt", "1@domain.com"),
                new Person("franz", "dgd", new DateTime(2002, 9, 11)),
                new Person("r", "nbvx", "k@domain.com", new DateTime(1923, 5, 16)),
                new Person("rfg", "ry", "krt@domain.com"),
                new Person("leo", "qwnbv", "qwerrt@domain.com", new DateTime(1990, 12, 29)),
                new Person("dima", "mmv", "hgd@domain.com"),
                new Person("vladimir", "aargf", "asdfg@domain.com", new DateTime(1995, 12, 31)),
                new Person("michael", "zdf", "nnn@domain.com"),
                new Person("miles", "bfdas", "m@domain.com", new DateTime(1974, 6, 8)),
                new Person("zzdsg", "rtfhg", "rrrrrr@domain.com", new DateTime(1889, 6, 4)),
                new Person("fsgf", "yuhfd", "wee@domain.com"),
                new Person("gsfdg", "wewr", "weltistbreit@domain.com", new DateTime(1977, 6, 27)),
                new Person("sdirepo", "dfg", new DateTime(1955, 2, 1)),
                new Person("fdgvbc", "hdsk", "rert@domain.com"),
                new Person("eryt", "ssaa", "lll@domain.com", new DateTime(1966, 5, 19)),
                new Person("qwww", "tttn", "dsfdl@domain.com", new DateTime(1999, 10, 13)),
                new Person("cxx", "oop", "sss@domain.com", new DateTime(1983, 11, 17)),
                new Person("lana", "tgbbb", "x@domain.com"),
                new Person("rayan", "evcxb", "qqq@domain.com"),
                new Person("gtr", "wwggfb", "ppp@domain.com", new DateTime(1993, 3, 18)),
                new Person("nancy", "eret", "43543fvsf@domain.com", new DateTime(1933, 1, 31)),
                new Person("eee", "tt", "v.ttt@domain.com"),
                new Person("www", "aaaaaarrrr", "ddt@domain.com", new DateTime(2014, 2, 28)),
                new Person("kamila", "var", "ccddt@domain.com", new DateTime(1947, 7, 23)),
                new Person("olga", "f", "iuyt@domain.com"),
                new Person("olgafd", "lkjhgfd", "mnbv@domain.com", new DateTime(1962, 5, 22)),
                new Person("nanna", "assdefe", "kjhgf@domain.com", new DateTime(1885, 3, 23)),
                new Person("emfdl", "sdf", "rtgf@domain.com"),
                new Person("tnn", "drtg", "dfsg@domain.com", new DateTime(1984, 4, 1)),
                new Person("ernn", "gtyy", "aaaaaaaa@domain.com"),
                new Person("aasdsf", "tyuii", "vbvcb@domain.com", new DateTime(1969, 7, 5)),
                new Person("uhgg", "yui", "bestmail@domain.com", new DateTime(2013, 6, 6)),
                new Person("rethn", "oiuytr", "eeel@domain.com", new DateTime(2019, 1, 2)),
                new Person("c", "imbv", "hhh@domain.com"),
                new Person("d", "pghj", "hhrrrh@domain.com", new DateTime(1997, 4, 8)),
                new Person("kkl", "vjjjj", "srwerty@domain.com", new DateTime(2001, 9, 23)),
                new Person("rtet", "xsss", new DateTime(1925, 12, 4)),
                new Person("pivo", "erere", "ttnn@domain.com"),
                new Person("dan", "bbbgr", "n.434@domain.com", new DateTime(2000, 4, 30))
                };
                SaveChanges();
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
            }
        }

        public List<Person> PersonsList
        {
            get { return persons.ToList(); }
        }

        public void AddPerson(Person person)
        {
            persons.Add(person);
            SaveChanges();
        }

        public void RemovePerson(Person person)
        {
            var personToRemove = persons.First(p => p.Name == person.Name && p.LastName == person.LastName && p.Email == person.Email);
            persons.Remove(personToRemove);
            SaveChanges();
        }

        public void UpdatePerson(int index, Person person)
        {
            persons[index] = person;
            SaveChanges();
        }

        private void SaveChanges()
        {
            SerializationManager.Serialize(persons, FileFolderHelper.StorageFilePath);
        }
    }
}
