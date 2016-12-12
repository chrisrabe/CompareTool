using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List
{
    /// <summary>
    /// This utilises a list or an array structure internally to store the databases. 
    /// This database doesn't support removal operations.
    /// </summary>
    public interface IListDatabaseStorage : IDatabaseStorage
    {
        
        /// <summary>
        /// Gets or sets the database at the chosen index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IDatabase this[int index] { get; set; }
    }
}
