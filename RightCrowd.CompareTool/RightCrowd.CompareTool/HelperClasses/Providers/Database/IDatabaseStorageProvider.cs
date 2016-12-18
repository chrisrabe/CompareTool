using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;

namespace RightCrowd.CompareTool.HelperClasses.Providers.Database
{
    /// <summary>
    /// This class is responsible for storing the database storage.
    /// </summary>
    public interface IDatabaseStorageProvider
    {
        /// <summary>
        /// Gets or sets the database storage.
        /// </summary>
        IListDatabaseStorage DatabaseStorage { get; set; }
    }
}
