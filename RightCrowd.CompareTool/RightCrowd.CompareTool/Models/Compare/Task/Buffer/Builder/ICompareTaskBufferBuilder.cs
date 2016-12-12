using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.Models.Compare.Task.Buffer.Builder
{
    /// <summary>
    /// Builds a task buffer given the tasks.
    /// </summary>
    public interface ICompareTaskBufferBuilder
    {
        /// <summary>
        /// Takes multiple databases and
        /// stores them into a compare task.
        /// </summary>
        /// <param name="database"></param>
        void StoreTask(params IDatabase[] database);

        /// <summary>
        /// Retrieves the buffer which is built from the
        /// stored tasks.
        /// </summary>
        ICompareTaskBuffer Buffer { get; }
    }
}
