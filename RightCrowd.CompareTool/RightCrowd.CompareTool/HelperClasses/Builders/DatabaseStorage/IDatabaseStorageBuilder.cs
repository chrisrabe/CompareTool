using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage;

namespace RightCrowd.CompareTool.HelperClasses.Builders.DatabaseStorage
{
    /// <summary>
    /// This class is responsible for creating database storages.
    /// </summary>
    public interface IDatabaseStorageBuilder
    {
        /// <summary>
        /// Builds and then retrieves the storage.
        /// </summary>
        IDatabaseStorage Storage { get; }
    }
}
