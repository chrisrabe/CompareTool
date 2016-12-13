using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;

namespace RightCrowd.CompareTool.HelperClasses.Builders.DatabaseStorage.Map
{
    /// <summary>
    /// This class is responsible for creating map database storages.
    /// </summary>
    public interface IMapDatabaseStorageBuilder : IDatabaseStorageBuilder
    {
        /// <summary>
        /// Adds a database into the storage builder
        /// and maps it to the given key.
        /// </summary>
        /// <param name="key"></param>
        /// <param name="database"></param>
        void Add(string key, IDatabase database);

        /// <summary>
        /// Adds the data node into the database key.
        /// </summary>
        /// <param name="node"></param>
        /// <param name="databaseKey"></param>
        void AddNode(IDataNode node, string databaseKey);

        /// <summary>
        /// Assigns the database to the given key.
        /// </summary>
        /// <param name="newData"></param>
        /// <param name="key"></param>
        void Set(IDatabase newData, string key);

        /// <summary>
        /// Removes the database relating to the key.
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}
