using System.ComponentModel;

using RightCrowd.CompareTool.HelperClasses.CompareTask.Manager;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Task;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Helper;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;
using RightCrowd.CompareTool.Models.Comparison.Data;
using RightCrowd.CompareTool.Models.DataModels.DataNode;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Worker
{
    /// <summary>
    /// This is a worker for the task manager. It is assigned a task
    /// and executes the comparison logic in the background.
    /// </summary>
    public class CompareTaskWorker : ICompareTaskWorker
    {
        #region Fields

        private string _assignedDataType;
        private ICompareTask _task;
        private ICompareTaskManager _manager;
        private BackgroundWorker _worker;
        private ICompareTaskHelper _assistant;

        private IListDatabaseStorage _difference;
        private IListDatabaseStorage _similarities;

        #endregion // Fields

        #region Constructors

        public CompareTaskWorker(string assignedDataType, ICompareTaskManager manager)
        {
            _assignedDataType = assignedDataType;
            _manager = manager;
            _worker = new BackgroundWorker();
            _assistant = new CompareTaskHelper();
        }

        #endregion // Constructors

        #region Properties

        public ICompareTask Task
        {
            set
            {
                if (_task != value)
                    _task = value;
            }
        }

        #endregion // Properties

        #region Methods

        /// <summary>
        /// Creates a background worker and runs it asynchronously.
        /// </summary>
        public void DoTask()
        {
            // Initialise properties of the background worker
            _worker.WorkerSupportsCancellation = true;
            _worker.DoWork += (obj, e) => Compare(_task.Databases, e);
            _worker.RunWorkerCompleted += (obj, e) => ReportCompleted();
            // run the background worker
            _worker.RunWorkerAsync();
        }

        /// <summary>
        /// Stops the worker from comparing.
        /// </summary>
        public void Stop()
        {
            _worker.CancelAsync();
        }

        #endregion // Methods

        #region BackgroundWorker Methods

        /// <summary>
        /// Compares the databases inside.
        /// </summary>
        /// <param name="databases"></param>
        /// <param name="e"></param>
        private void Compare(IDatabase[] databases, DoWorkEventArgs e)
        {
            // So far only supports comparison between two databases.
            if (databases.Length > 2)
            {
                e.Cancel = true;
                return; // abort work because a lot of databases detected.
            }
            // Retrieve both databases
            IDatabase db1 = databases[0];
            IDatabase db2 = databases[1];
            // Iterate through each node of the first database
            foreach (IDataNode node in db1.Data)
            {
                IDataNode other = _assistant.GetOther(node, db2);
            }
        }

        /// <summary>
        /// Gives the gathered data to the manager and
        /// reports that the task has been completed.
        /// </summary>
        public void ReportCompleted()
        {
            IComparisonData data = new ComparisonData(_assignedDataType);
            data.Difference = _difference;
            data.Similarities = _similarities;
            _manager.SubmitData(data);
        }

        #endregion // BackgroundWorker Methods
    }
}
