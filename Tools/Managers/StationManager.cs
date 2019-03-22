using lab4_cs.Models;
using lab4_cs.Tools.DataStorage;
using System;

namespace lab4_cs.Tools.Managers
{
    internal static class StationManager
    {
        private static IDataStorage _dataStorage;
        
        internal static Person CurrentPerson {get; set;}

        internal static IDataStorage DataStorage
        {
            get { return _dataStorage; }
        }

        internal static void Initialize(IDataStorage dataStorage)
        {
            _dataStorage = dataStorage;
        }

        internal static void CloseApp()
        {
            Environment.Exit(1);
        }
    }
}
