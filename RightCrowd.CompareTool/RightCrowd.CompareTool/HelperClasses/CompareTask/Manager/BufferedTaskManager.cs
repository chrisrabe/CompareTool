using System.Collections.Generic;
using RightCrowd.CompareTool.HelperClasses.Builders.TaskBuffer;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Buffer;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Task;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Worker;
using RightCrowd.CompareTool.HelperClasses.EventHandlers.Compare;
using RightCrowd.CompareTool.Models.Comparison.Data;
using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.Map;
using System.Runtime.CompilerServices;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Manager
{
    /// <summary>
    /// This compare task manager puts all the work inside a task buffer
    /// and forwards each task to its workers.
    /// </summary>
    public class BufferedTaskManager : ICompareTaskManager
    {
        #region Fields
        // Fields to keep track of progress
        private int _totalTasks = 0;
        private int _completeTasks = 0;
        // Fields needed for the comparison
        private IComparisonDataStorage _storage;
        private ICompareEventHandler _handler;
        private ICompareTaskBuffer _buffer;

        #endregion // Fields

        #region Constructor

        public BufferedTaskManager(ICompareEventHandler handler)
        {
            _handler = handler;
            _storage = new ComparisonDataStorage();
        }

        #endregion // Contructor

        #region Methods

        /// <summary>
        /// Wraps each items inside the partition inside a CompareTask and places them inside
        /// a task buffer and forwards it to the compare task worker.
        /// </summary>
        /// <param name="partitions"></param>
        /// <returns></returns>
        public void Compare(params IMapDatabaseStorage[] partitions)
        {
            _buffer = CreateBuffer(partitions);
            _totalTasks = _buffer.Count;

            while (!_buffer.Done)
            {
                ICompareTask task = _buffer.Next;
                if (task != null) // buffer.Next could return null
                {
                    ICompareTaskWorker worker = new CompareTaskWorker(task.Type, this);
                    worker.Task = task;
                    worker.DoTask();
                }
            }
        }

        /// <summary>
        /// Records the submitted data and then reports the progress to
        /// the event handler. This method is synchronised to avoid
        /// multiple threads submitting at the same time.
        /// </summary>
        /// <param name="data"></param>
        [MethodImpl(MethodImplOptions.Synchronized)]
        public void SubmitData(IComparisonData data)
        {
            _storage.ComparisonData.Add(data);
            _completeTasks++;
            int progress = (_completeTasks / _totalTasks) * 100;
            _handler.ReportProgress(progress);
            if (progress == 100)
                _handler.SubmitStorage(_storage);
        }

        #endregion // Methods

        #region Helper Methods

        /// <summary>
        /// This method creates the buffer from the partitions.
        /// </summary>
        /// <param name="partitions"></param>
        /// <returns></returns>
        private ICompareTaskBuffer CreateBuffer(IMapDatabaseStorage[] partitions)
        {
            ICompareTaskBufferBuilder builder = new ListTaskBufferBuilder();
            List<string> tasksCreated = new List<string>(); // keeps track of the tasks created
            int db1 = 0, db2 = 1;
            // Iterate through database one
            CreateTasks(db1, db2, db1, builder, tasksCreated, partitions);
            // Iterate through database two
            CreateTasks(db1, db2, db2, builder, tasksCreated, partitions);
            return builder.Buffer;
        }

        /// <summary>
        /// Adds tasks to the buffer by iterating through the subject's keys and then adding
        /// them into the builder.
        /// </summary>
        /// <param name="db1"></param>
        /// <param name="db2"></param>
        /// <param name="subject"></param>
        /// <param name="builder"></param>
        /// <param name="tasksCreated"></param>
        /// <param name="partitions"></param>
        private void CreateTasks(int db1, int db2, int subject, ICompareTaskBufferBuilder builder, List<string> tasksCreated, IMapDatabaseStorage[] partitions)
        {
            foreach (string key in partitions[subject].Keys)
            {
                if (!tasksCreated.Contains(key))
                {
                    IDatabase database1 = partitions[db1][key];
                    IDatabase database2 = partitions[db2][key];
                    builder.Add(new Task.CompareTask(key, database1, database2));
                    tasksCreated.Add(key);
                }
            }
        }

        #endregion // Helper Methods
    }
}
