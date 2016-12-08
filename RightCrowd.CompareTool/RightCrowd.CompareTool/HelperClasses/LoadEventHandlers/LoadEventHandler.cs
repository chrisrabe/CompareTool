using RightCrowd.CompareTool.HelperClasses.Readers.XMLReaders;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using System.ComponentModel;
using System.IO;
using System.Windows;

namespace RightCrowd.CompareTool.HelperClasses.LoadEventHandlers
{
    /// <summary>
    /// This class loads the database in the background while reporting
    /// The progress back to the LoadViewModel. When the background task
    /// is complete, it automatically places the newly created database
    /// into ApplicationViewModel's database storage.
    /// </summary>
    public class LoadEventHandler
    {
        private IDatabase _database;
        private int _databaseIndex;
        private BackgroundWorker _worker;
        private LoadViewModel _viewModel;

        public LoadEventHandler(LoadViewModel viewModel)
        {
            _worker = new BackgroundWorker();
            _viewModel = viewModel;
        }
        /// <summary>
        /// Creates a background worker thread
        /// </summary>
        /// <returns></returns>
        public void LoadDirectory(string directoryPath, int databaseIndex)
        {
            _databaseIndex = databaseIndex;
            UpdateProgress(0);
            _worker.DoWork += (obj, e) => LoadDirectory(directoryPath);
            _worker.WorkerReportsProgress = true;
            _worker.ProgressChanged += (obj,e) => UpdateProgress(e.ProgressPercentage);
            _worker.RunWorkerCompleted += AddDatabaseToStorage;

            _worker.RunWorkerAsync();
        }

        private void AddDatabaseToStorage(object sender, RunWorkerCompletedEventArgs e)
        {
            ApplicationViewModel.Instance.DatabaseStorage[_databaseIndex] = _database;
            UpdateProgress(100);
            MessageBox.Show(string.Format("Database {0} has been loaded.", (_databaseIndex + 1)));
        }

        private void UpdateProgress(int newValue)
        {
            if (_databaseIndex == 0)
                _viewModel.LoadDB1Progress = newValue;
            else
                _viewModel.LoadDB2Progress = newValue;
        }

        private void LoadDirectory(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath);
            _database = new Database(directoryPath);
            IXMLReader xmlReader = new XMLReader();
            int numFiles = files.Length;
            int numProcessed = 0;

            foreach(string file in files)
            {
                if (file.EndsWith(".xml"))
                {
                    IDataNode node = xmlReader.ReadXMLFile(file);
                    if (node != null)
                        _database.Data.Add(node);
                }
                numProcessed++;
                _worker.ReportProgress((numProcessed / numFiles) * 100);
            }
        }
    }
}
