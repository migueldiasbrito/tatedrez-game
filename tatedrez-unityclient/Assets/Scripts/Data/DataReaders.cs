using System;
using System.Collections.Generic;

namespace Mdb.Tatedrez.Data
{
    public static class DataReaders
    {
        private static Dictionary<Type, IDataReader> _dataReaders = new();

        public static T Get<T>() where T : IDataReader
        {
            return (T)_dataReaders[typeof(T)];
        }

        public static void Bind<T>(IDataReader dataReader)
        {
            _dataReaders.Add(typeof(T), dataReader);
        }
    }
}
