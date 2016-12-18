using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;

namespace RightCrowd.CompareTool.HelperClasses.Providers.Database
{
    /// <summary>
    /// This class is responsible for caching the database storage.
    /// </summary>
    internal class DatabaseStorageProvider : IDatabaseStorageProvider
    {
        #region Fields

        private IListDatabaseStorage _storage;

        #endregion // Fields

        #region Properties

        public IListDatabaseStorage DatabaseStorage
        {
            get
            {
                if (_storage == null)
                    _storage = new TwoDatabaseStorage();

                return _storage;
            }

            set
            {
                if (_storage != value)
                    _storage = value;
            }
        }

        #endregion // Properties
    }
}
