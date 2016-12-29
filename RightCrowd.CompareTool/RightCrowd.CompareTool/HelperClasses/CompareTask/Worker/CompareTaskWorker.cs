using System.Linq;
using System.ComponentModel;

using RightCrowd.CompareTool.Models.Comparison.Data;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Task;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Manager;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataComparators;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataHandlers;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;
using RightCrowd.CompareTool.HelperClasses.MetaData;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Worker
{
    /// <summary>
    /// This is a worker for the task manager. This worker is responsible
    /// for creating a background worker for the comparison logic.
    /// </summary>
    public class CompareTaskWorker : ICompareTaskWorker
    {
        #region Fields

        private string _assignedDataType;
        private ICompareTask _task;
        private ICompareTaskManager _manager;
        private IDataComparator _comparator;
        private BackgroundWorker _worker;
        private IMetaData _nodeMetaData;

        #endregion // Fields

        #region Constructors

        public CompareTaskWorker(string assignedDataType, ICompareTaskManager manager)
        {
            _assignedDataType = assignedDataType;
            _manager = manager;
            _worker = new BackgroundWorker();
            _nodeMetaData = new NodeMetaData();
            var mapping = _nodeMetaData.KeyFields.FirstOrDefault(x => x.DatabaseName.StartsWith(assignedDataType));
            if (mapping == null || !(mapping is DatabaseKeyField))
                _comparator = new DataComparator();
            else
                _comparator = ((DatabaseKeyField)mapping).DataComparator;
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
            _worker.DoWork += (obj, e) => Compare(_task.Databases);
            _worker.RunWorkerCompleted += (obj, e) => ReportCompleted();
            // run the background worker
            _worker.RunWorkerAsync();
        }

        #endregion // Methods

        #region BackgroundWorker Methods

        /// <summary>
        /// Compares the databases inside.
        /// </summary>
        /// <param name="databases"></param>
        /// <param name="e"></param>
        private void Compare(IDatabase[] databases)
        {
            int db1 = 0, db2 = 1;
            _comparator.Handler = new DataHandler(databases);
            SetNodesNotVisited(databases[db1]);
            SetNodesNotVisited(databases[db2]);
            // First compare database one against database two
            _comparator.Compare(db1, db2, databases[db1], databases[db2]);
            // Next compare database two against database one
            _comparator.Compare(db2, db1, databases[db1], databases[db2]);
        }

        /// <summary>
        /// Gives the gathered data to the manager and
        /// reports that the task has been completed.
        /// </summary>
        public void ReportCompleted()
        {
            IComparisonData data = new ComparisonData(_assignedDataType);
            data.Difference = (IListDatabaseStorage)_comparator.Handler.Differences.Storage;
            data.Similarities = (IListDatabaseStorage)_comparator.Handler.Similarities.Storage;
            _manager.SubmitData(data);
        }

        #endregion // BackgroundWorker Methods

        #region Helper Methods

        /// <summary>
        /// Sets the Visited property of all the nodes property to false. This is important in case
        /// the user wants to compare a database twice.
        /// </summary>
        /// <param name="database"></param>
        private void SetNodesNotVisited(IDatabase database)
        {
            database.Data.ToList().ForEach(node => { node.Visited = false; });
        }

        #endregion // Helper Methods
    }
}
