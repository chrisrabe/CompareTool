using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.Models.DataModels.DatabaseStorage
{
    /// <summary>
    /// Stores a collection of databases.
    /// </summary>
    public interface IDatabaseStorage
    {
        /// <summary>
        /// Retrieves all the database inside the storage.
        /// </summary>
        IDatabase[] Databases { get; }
    }
}
