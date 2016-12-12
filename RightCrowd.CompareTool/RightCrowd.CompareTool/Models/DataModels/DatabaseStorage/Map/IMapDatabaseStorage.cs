using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.Map
{
    /// <summary>
    /// Stores databases and maps them to a key string.
    /// </summary>
    interface IMapDatabaseStorage : IDatabaseStorage
    {
        /// <summary>
        /// Gets or sets the database based on the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IDatabase this[string key] { get; set; }
    }
}
