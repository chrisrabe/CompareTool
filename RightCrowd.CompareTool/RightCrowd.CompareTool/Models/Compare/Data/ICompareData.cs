using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage;

namespace RightCrowd.CompareTool.Models.Compare.Data
{
    /// <summary>
    /// Contains two storages representing the differences 
    /// and similarities of multiple databases of a specific type.
    /// </summary>
    public interface ICompareData
    {
        /// <summary>
        /// gets or sets the type of data nodes compared.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Contains databases which indicates which values are
        /// not inside the other databases.
        /// </summary>
        IDatabaseStorage Differences { get; set; }

        /// <summary>
        /// Contains databases which indicates which values are
        /// inside the other databases.
        /// </summary>
        IDatabaseStorage Similarities { get; set; }
    }
}
