using RightCrowd.CompareTool.Models.DataModels.Database;
using System.Collections.Generic;

namespace RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.Map
{
    /// <summary>
    /// Stores databases and maps them to a key string.
    /// </summary>
    public interface IMapDatabaseStorage : IDatabaseStorage
    {
        /// <summary>
        /// Gets or sets the database based on the key.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        IDatabase this[string key] { get; set; }
        
        /// <summary>
        /// Returns all the keys in this map database storage.
        /// </summary>
        List<string> Keys { get; }
    }
}
