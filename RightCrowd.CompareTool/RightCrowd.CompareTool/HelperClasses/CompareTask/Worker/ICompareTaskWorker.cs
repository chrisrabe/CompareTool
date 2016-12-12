﻿using RightCrowd.CompareTool.HelperClasses.CompareTask.Manager;
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

        void DoTask(ICompareTaskManager manager);

        /// <summary>
        /// Reports to the manager that it has completed its task.
        /// </summary>
        void ReportProgress();
    }
}