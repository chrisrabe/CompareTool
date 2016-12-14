using RightCrowd.CompareTool.HelperClasses.CompareTask.Task;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Worker
{
    /// <summary>
    /// This class compares the partitioned data from the compare task
    /// in the background. The compare results will be stored inside a
    /// comaprison data object.
    /// </summary>
    public interface ICompareTaskWorker
    {
        ICompareTask Task { set; }

        /// <summary>
        /// Compares the databases inside the task.
        /// </summary>
        void DoTask();

        /// <summary>
        /// Reports to the manager that it has completed its task.
        /// </summary>
        void ReportCompleted();
    }
}
