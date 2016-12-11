using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.Models.DataModels.DatabaseStorage
{
    /// <summary>
    /// Provides a skeletal implementation of a database storage.
    /// </summary>
    public interface IDatabaseStorage
    {
        /// <summary>
        /// Retrieves all the database inside the storage.
        /// </summary>
        IDatabase[] Databases { get; }
    }
}
