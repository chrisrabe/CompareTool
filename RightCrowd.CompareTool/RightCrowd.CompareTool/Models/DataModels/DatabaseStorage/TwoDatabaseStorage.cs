using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.Models.DataModels.Database;
using System;

namespace RightCrowd.CompareTool.Models.DataModels.DatabaseStorage
{
    /// <summary>
    /// This class is is a storage for exactly two databases.
    /// </summary>
    public class TwoDatabaseStorage : ObservableObject, IDatabaseStorage
    {
        private const int STORAGE_SIZE = 2;
        private IDatabase[] _databases;

        public TwoDatabaseStorage()
        {
            _databases = new IDatabase[STORAGE_SIZE];
        }

        #region Properties

        public string DirectoryOne
        {
            get
            {
                return _databases[0] == null ? "" : _databases[0].DirectoryName;
            }
        }

        public string DirectoryTwo
        {
            get
            {
                return _databases[1] == null ? "" : _databases[1].DirectoryName;
            }
        }

        #endregion // Properties

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
    }
}
