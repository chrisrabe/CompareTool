﻿using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List
{
    /// <summary>
    /// This class is is a storage for exactly two databases.
    /// </summary>
    public class TwoDatabaseStorage : ObservableObject, IListDatabaseStorage
    {
        private const int STORAGE_SIZE = 2;
        private IDatabase[] _databases;

        public TwoDatabaseStorage()
        {
            _databases = new IDatabase[STORAGE_SIZE];
        }

        #region IDatabase Members

        public IDatabase this[int index]
        {
            get
            {
                return _databases[index];
            }

            set
            {
                _databases[index] = value;
            }
        }

        public IDatabase[] Databases
        {
            get
            {
                return _databases;
            }
        }

        #endregion // IDatabase Members

        #region Object methods

        public override string ToString()
        {
            return string.Format("{0} - {1}", Databases[0].DirectoryName, Databases[1].DirectoryName);
        }

        #endregion
    }
}
