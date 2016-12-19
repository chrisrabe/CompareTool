using System.Collections.Generic;
using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Manager;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.Map;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using System;
using System.Windows;
using RightCrowd.CompareTool.HelperClasses.Providers.CompareData;

namespace RightCrowd.CompareTool.HelperClasses.EventHandlers.Compare
{
    /// <summary>
    /// This task is responsible for updating the compare property of the
    /// loadViewModel when report progress is called.
    /// </summary>
    public class CompareEventHandler : ICompareEventHandler
    {
        #region Fields

        private LoadViewModel _viewModel;
        private ICompareTaskManager _manager;
        private ICompareDataProvider _compareDataProvider;

        #endregion // Fields

        #region Constructor

        /// <summary>
        /// Creates and instance of a compare event handler
        /// </summary>
        /// <param name="viewModel"></param>
        public CompareEventHandler(LoadViewModel viewModel)
        {
            _viewModel = viewModel;
            _manager = new BufferedTaskManager(this);
        }

        public CompareEventHandler(LoadViewModel viewModel, ICompareDataProvider compareDataProvider) : this(viewModel)
        {
            _compareDataProvider = compareDataProvider;
        }

        #endregion // Constructor

        #region Methods

        /// <summary>
        /// Parititons the storage passed and then passes those partitions to the
        /// compare task manager.
        /// </summary>
        /// <param name="storage"></param>
        /// <returns></returns>
        public void Compare(IListDatabaseStorage storage)
        {
            int db1 = 0, db2 = 1;
            IMapDatabaseStorage partition1 = PartitionDatabase(storage.Databases[db1]);
            IMapDatabaseStorage partition2 = PartitionDatabase(storage.Databases[db2]);
            _manager.Compare(partition1, partition2);
        }

        public void ReportProgress(int progress)
        {
            if(_viewModel != null)
                _viewModel.CompareProgress = progress;
        }

        public void SubmitStorage(IComparisonDataStorage storage)
        {
            _compareDataProvider.ComparisonStorage = storage;
            MessageBox.Show("Databases Compared");
        }

        #endregion // Methods

        #region Helper Methods

        /// <summary>
        /// Separates each node inside the given database into their types and returns
        /// a map database storage.
        /// </summary>
        /// <param name="database"></param>
        /// <returns></returns>
        private IMapDatabaseStorage PartitionDatabase(IDatabase database)
        {
            Dictionary<string, IDatabase> partition = new Dictionary<string, IDatabase>();
            IMapDatabaseStorage partitionStorage = new SubDatabaseStorage();
            foreach (IDataNode data in database.Data)
            {
                string type = GetType(data);
                IDatabase tmp;
                if (!partition.TryGetValue(type, out tmp))
                {
                    tmp = new Database(database.DirectoryName);
                    partition.Add(type, tmp);
                }

                tmp.Data.Add(data);
            }
            foreach (KeyValuePair<string, IDatabase> entry in partition)
            {
                partitionStorage[entry.Key] = entry.Value;
            }
            return partitionStorage;
        }

        /// <summary>
        /// Retrieves the data node type from its file name.
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string GetType(IDataNode data)
        {
            string filename = data.FileName;
            string[] split = filename.Split('.');
            return split[0]; // First part should be the type
        }

        #endregion // Helper Methods
    }
}
