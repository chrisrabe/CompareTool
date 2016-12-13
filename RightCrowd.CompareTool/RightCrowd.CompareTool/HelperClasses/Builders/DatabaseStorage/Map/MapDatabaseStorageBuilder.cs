using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using System.Collections.Generic;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.Map;

namespace RightCrowd.CompareTool.HelperClasses.Builders.DatabaseStorage.Map
{
    /// <summary>
    /// This class creates a map database storage.
    /// </summary>
    public class MapDatabaseStorageBuilder : IMapDatabaseStorageBuilder
    {
        #region Fields

        private Dictionary<string, IDatabase> _databases;

        #endregion // Fields

        #region Constructor

        public MapDatabaseStorageBuilder()
        {
            _databases = new Dictionary<string, IDatabase>();
        }

        #endregion

        #region Properties

        /// <summary>
        /// Builds a sub database storage.
        /// </summary>
        public IDatabaseStorage Storage
        {
            get
            {
                IMapDatabaseStorage storage = new SubDatabaseStorage();
                foreach(KeyValuePair<string, IDatabase> entry in _databases)
                {
                    storage[entry.Key] = entry.Value;
                }
                return storage;
            }
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Adds the new database and maps it to the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        public void Add(string key, IDatabase database)
        {
            _databases.Add(key, database);
        }

        /// <summary>
        /// Adds a node to the database with the databaseKey.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="databaseKey"></param>
        public void AddNode(IDataNode node, string databaseKey)
        {
            IDatabase database;
            if(_databases.TryGetValue(databaseKey, out database)) // Check if key exists
            {
                database.Data.Add(node);
            }
        }

        #endregion // Methods
    }
}
