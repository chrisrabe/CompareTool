using RightCrowd.CompareTool.Models.Compare.Data;
using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.Models.Compare.Task
{
    /// <summary>
    /// This class is responsible for comparing the databases.
    /// </summary>
    public interface ICompareTask
    {
        /// <summary>
        /// Sets the databases for this task.
        /// </summary>
        IDatabase[] Databases { set; }

        /// <summary>
        /// Returns the comparison data.
        /// </summary>
        ICompareData Data { get; }
    }
}
