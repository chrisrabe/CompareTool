using System;
using System.IO;
using System.Windows;
using RightCrowd.CompareTool.HelperClasses.Readers.XML;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using System.ComponentModel;
using RightCrowd.CompareTool.HelperClasses.Providers.Database;

namespace RightCrowd.CompareTool.HelperClasses.EventHandlers.Load
{
    /// <summary>
    /// This class loads the database in the background while reporting
    /// The progress back to the LoadViewModel. When the background task
    /// is complete, it automatically places the newly created database
    /// into ApplicationViewModel's database storage.
    /// 
    /// This event handler only works with two databases.
    /// </summary>
    public class LoadEventHandler : ILoadEventHandler
    {
        #region Fields

        private int _databaseIndex;
        private IDatabase _database;
        private BackgroundWorker _worker;
        private LoadViewModel _viewModel;
        private IDatabaseStorageProvider _databaseProvider;

        #endregion // Fields

        #region Constructors

        public LoadEventHandler(LoadViewModel viewModel)
        {
            _worker = new BackgroundWorker();
            _viewModel = viewModel;
        }

        public LoadEventHandler(LoadViewModel viewModel, IDatabaseStorageProvider _databaseProvider) : this(viewModel)
        {
            this._databaseProvider = _databaseProvider;
        }

        #endregion // Constructors

        #region Methods

        /// <summary>
        /// Creates a background worker thread
        /// </summary>
        /// <returns></returns>
        public void LoadDirectory(string directoryPath, int databaseIndex)
        {
            _databaseIndex = databaseIndex;
            // Initialise properties of the view model
            UpdateProgress(0);
            // Initialise properties of the background worker
            _worker.DoWork += (obj, e) => LoadDirectory(directoryPath, e);
            _worker.WorkerReportsProgress = true;
            _worker.WorkerSupportsCancellation = true;
            _worker.ProgressChanged += (obj, e) => UpdateProgress(e.ProgressPercentage);
            _worker.RunWorkerCompleted += AddDatabaseToStorage;
            // run the worker
            _worker.RunWorkerAsync();
        }

        /// <summary>
        /// Aborts any worker processes.
        /// </summary>
        public void StopLoading()
        {
            _worker.CancelAsync();
        }

        #endregion // Methods

        #region Worker Methods
        private void AddDatabaseToStorage(object sender, RunWorkerCompletedEventArgs e)
        {
            if (_database != null)
            {
                _databaseProvider.DatabaseStorage[_databaseIndex] = _database;
                SetDirectory(_database.DirectoryName);
                UpdateProgress(100);
                MessageBox.Show(String.Format("Database {0} has been loaded.", (_databaseIndex + 1)));
                _viewModel.CheckIfDatabaseLoaded();
            }
            else
                MessageBox.Show("Database cannot be loading because no xml files detected");
        }

        private void LoadDirectory(string directoryPath, DoWorkEventArgs e)
        {
            string[] files = Directory.GetFiles(directoryPath);
            _database = new Database(directoryPath);
            IXMLReader xmlReader = new XMLReader();
            int numFiles = files.Length;
            int xmlProcessed = 0; // keeps track of xml files read in total.
            int numProcessed = 0; // keeps track of files read in total.

            foreach (string file in files)
            {
                if (file.EndsWith(".xml"))
                {
                    IDataNode node = xmlReader.ReadXMLFile(file);
                    if (node != null)
                    {
                        xmlProcessed++;
                        _database.Data.Add(node);
                    }
                }
                numProcessed++;
                _worker.ReportProgress((numProcessed / numFiles) * 100);
                // Check if the user wants to cancel the previous load...
                if (_worker.CancellationPending)
                {
                    e.Cancel = true;
                    return; // abort work if it's cancelled.
                }
            }

            if (xmlProcessed == 0) // no xml files processed..
                _database = null; // no point keeping the database object
        }

        #endregion // Worker Methods

        #region Helper Methods

        /// <summary>
        /// Updates the directory path related to the data index inside the view model.
        /// </summary>
        /// <param name="directoryPath"></param>
        private void SetDirectory(string directoryPath)
        {
            if (_databaseIndex == 0)
                _viewModel.DirectoryOne = directoryPath;
            else
                _viewModel.DirectoryTwo = directoryPath;
        }

        /// <summary>
        /// Updates the progress value related to the data index inside the view model.
        /// </summary>
        /// <param name="newValue"></param>
        private void UpdateProgress(int newValue)
        {
            if (_databaseIndex == 0)
                _viewModel.LoadDB1Progress = newValue;
            else
                _viewModel.LoadDB2Progress = newValue;
        }

        #endregion // Helper Methods
    }
}
