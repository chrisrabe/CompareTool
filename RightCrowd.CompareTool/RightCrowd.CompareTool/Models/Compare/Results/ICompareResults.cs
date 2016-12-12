using RightCrowd.CompareTool.Models.Compare.Data;

namespace RightCrowd.CompareTool.Models.Compare.Results
{
    /// <summary>
    /// This class should provide a way to store compare data objects.
    /// </summary>
    public interface ICompareResults
    {
        /// <summary>
        /// Stores a compare data node.
        /// </summary>
        /// <param name="data"></param>
        void Add(ICompareData data);

        /// <summary>
        /// Retrieves the results from the comparison.
        /// </summary>
        ICompareData[] Results { get; }
    }
}
