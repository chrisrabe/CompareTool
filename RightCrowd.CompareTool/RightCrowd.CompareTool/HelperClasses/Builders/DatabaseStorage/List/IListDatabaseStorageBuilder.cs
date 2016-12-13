using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;

namespace RightCrowd.CompareTool.HelperClasses.Builders.DatabaseStorage.List
{   
    /// <summary>
    /// This storage builder supports methods which can create
    /// a list database storage.
    /// </summary>
    public interface IListDatabaseStorageBuilder : IDatabaseStorageBuilder
    {
        /// <summary>
        /// Adds a database into the storage builder.
        /// </summary>
        /// <param name="database"></param>
        void Add(IDatabase database);

        /// <summary>
        /// Add data node into the database index.
        /// </summary>
        void AddNode(IDataNode node, int databaseIndex);

        /// <summary>
        /// Swap a database from one index to another.
        /// </summary>
        void Swap(int from, int to);

        /// <summary>
        /// Replace database at a given index.
        /// </summary>
        void Set(IDatabase newDatabase, int index);

        /// <summary>
        /// Remove database at a given index.
        /// </summary>
        void Remove(int index);
    }
}
