using RightCrowd.CompareTool.Models.Display.Data;

namespace RightCrowd.CompareTool.Models.Display.Partition
{
    /// <summary>
    /// Stores the comparison result for one database. In the compare view,
    /// The data stored in this class is used in one of the panel partitions.
    /// </summary>
    public interface IDisplayPartition
    {
        /// <summary>
        /// Gets or sets the differences
        /// </summary>
        IDisplayData Differences { get; set; }

        /// <summary>
        /// Gets or sets the similarities
        /// </summary>
        IDisplayData Similarities { get; set; }
    }
}
