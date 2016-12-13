using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using System.Collections.Generic;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;

namespace RightCrowd.CompareTool.HelperClasses.Builders.DatabaseStorage.List
{
    /// <summary>
    /// Creates a two database storage.
    /// </summary>
    public class ListDatabaseStorageBuilder : IListDatabaseStorageBuilder
    {
        #region Fields

        private List<IDatabase> _databases;

        #endregion // Fields

        #region Constructor

        public ListDatabaseStorageBuilder()
        {
            _databases = new List<IDatabase>();
        }

        #endregion // Constructor

        #region Properties

        /// <summary>
        /// Builds the database storage and retrieves it. This property
        /// returns a TwoDatabaseStorage object.
        /// </summary>
        public IDatabaseStorage Storage
        {
            get
            {
                // Create the database storage
                IListDatabaseStorage storage = new TwoDatabaseStorage();
                // Initialise the databases built
                storage[0] = _databases[0];
                storage[1] = _databases[1];
                return storage;
            }
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Adds the database into the builder.
        /// </summary>
        /// <param name="database"></param>
        public void Add(IDatabase database)
        {
            _databases.Add(database);
        }

        /// <summary>
        /// Adds the node to the database at the given index.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="databaseIndex"></param>
        public void AddNode(IDataNode node, int databaseIndex)
        {
            if(0 <= databaseIndex && databaseIndex < _databases.Count)
            {
                _databases[databaseIndex].Data.Add(node);
            }
        }

        #endregion // Methods
    }
}
