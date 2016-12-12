using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;

namespace RightCrowd.CompareTool.Models.Comparison.Data
{
    /// <summary>
    /// Stores two types of data storages. One storage contain databases 
    /// which stores all the data nodes which are different from the compared databases.
    /// One storage contains databases which stores all data nodes which are similar
    /// from the compared databases.
    /// 
    /// The comparison data represents a collection of a single data node type of
    /// two or more databases.
    /// </summary>
    public interface IComparisonData
    {
        /// <summary>
        /// The data node type which this comparison data is representing.
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets a database storage which contains all the nodes which are
        /// different to all compared databases.
        /// </summary>
        IListDatabaseStorage Difference { get; set; }

        /// <summary>
        /// Gets or sets a database storage which contains all nodes which are 
        /// similar to all compared databases.
        /// </summary>
        IListDatabaseStorage Similarities { get; set; }
    }
}
