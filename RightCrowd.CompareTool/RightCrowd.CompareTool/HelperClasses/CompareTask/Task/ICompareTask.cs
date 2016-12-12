using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Task
{
    /// <summary>
    /// Contains partitioned data from different databases.
    /// Each partitioned data must contain data nodes of the
    /// same type.
    /// </summary>
    public interface ICompareTask
    {
        /// <summary>
        /// Gets or sets the database inside this compare task.
        /// </summary>
        IDatabase[] Databases { get; set; }
    }
}
