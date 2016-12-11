using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.HelperClasses.EventHandlers.Load;
using RightCrowd.CompareTool.Models.DataModels.Database;
using System.Collections.Generic;
using System.Windows.Forms;
using System.Windows.Input;

namespace RightCrowd.CompareTool
{
    public class LoadViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        // Processes
        private Dictionary<int, LoadEventHandler> _loadEvents;

        // Directories
        private string _directoryOne;
        private string _directoryTwo;

        // Progress bar fields
        private int _loadDB1Progress;
        private int _loadDB2Progress;
        private int _compareProgress;

        // Commands
        private ICommand _loadDatabaseOneCommand;
        private ICommand _loadDatabaseTwoCommand;
        private ICommand _compareDatabaseCommand;

        // Validation Checks
        private bool _databasesLoaded; // True if database storage has two databases

        #endregion // Fields

        #region Constructors

        public LoadViewModel()
        {
            _loadEvents = new Dictionary<int, LoadEventHandler>();
        }

        #endregion // Constructors

        #region ICommands

        public ICommand LoadDatabaseOne
        {
            get
            {
                if (_loadDatabaseOneCommand == null)
                    _loadDatabaseOneCommand = new RelayCommand(i => LoadDatabase(0));

                return _loadDatabaseOneCommand;
            }
        }

        public ICommand LoadDatabaseTwo
        {
            get
            {
                if (_loadDatabaseTwoCommand == null)
                    _loadDatabaseTwoCommand = new RelayCommand(i => LoadDatabase(1));

                return _loadDatabaseTwoCommand;
            }
        }

        #endregion // ICommands

        #region Database Related Properties

        public bool DatabasesLoaded
        {
            get { return _databasesLoaded; }
            set
            {
                if(_databasesLoaded != value)
                {
                    _databasesLoaded = value;
                    OnPropertyChanged("DatabasesLoaded");
                }
            }
        }

        public string DirectoryOne
        {
            get { return _directoryOne; }
            set
            {
                if(_directoryOne != value)
                {
                    _directoryOne = value;
                    OnPropertyChanged("DirectoryOne");
                }
            }
        }

        public string DirectoryTwo
        {
            get { return _directoryTwo; }
            set
            {
                if(_directoryTwo != value)
                {
                    _directoryTwo = value;
                    OnPropertyChanged("DirectoryTwo");
                }
            }
        }

        #endregion // Database Related Properties

        #region Progress Bar Properties

        // Note : If you want to add more progress bars, you have to 
        // create a property which binds to that progress bar's progress.

        public int LoadDB1Progress
        {
            get { return _loadDB1Progress; }
            set
            {
                if(this._loadDB1Progress != value)
                {
                    this._loadDB1Progress = value;
                    OnPropertyChanged("LoadDB1Progress");
                }
            }
        }

        public int LoadDB2Progress
        {
            get { return _loadDB2Progress; }
            set
            {
                if (this._loadDB2Progress != value)
                {
                    this._loadDB2Progress = value;
                    OnPropertyChanged("LoadDB2Progress");
                }
            }
        }

        public int CompareProgress
        {
            get { return _compareProgress; }
            private set
            {
                if(this._compareProgress != value)
                {
                    this._compareProgress = value;
                    OnPropertyChanged("CompareProgress");
                }
            }
        }

        #endregion // Progress Bar Properties

        #region IPageViewModel Members

        public string ImagePath
        {
            get { return "pack://application:,,,/Resources/load_view-logo.png"; }
        }

        public string Name
        {
            get
            {
                return "Load Databases";
            }
        }

        #endregion // IPageViewModel Members

        #region Methods

        /// <summary>
        /// Checks if the storage inside the application view model is full.
        /// This method sets the '_databaseLoaded' flag to true if the database storage is full.
        /// </summary>
        public void CheckIfDatabaseLoaded()
        {
            IDatabase[] databases = ApplicationViewModel.Instance.DatabaseStorage.Databases;
            for(int i = 0; i < databases.Length; i++)
            {
                if(databases[i] == null)
                {
                    DatabasesLoaded = false;
                    return; // at least one database is empty...
                }
            }
            DatabasesLoaded = true;
        }

        private void LoadDatabase(int databaseIndex)
        {
            FolderBrowserDialog browser = new FolderBrowserDialog();
            if(browser.ShowDialog() == DialogResult.OK)
            {
                // Stop any event handlers at that particular database
                LoadEventHandler handler;
                if(_loadEvents.TryGetValue(databaseIndex, out handler))
                {
                    // Handler detected. Stop the old handler..
                    handler.StopLoading();
                    // Lose reference to the handler so that it's garbage collected.
                    _loadEvents.Remove(databaseIndex);
                }
                // Create a new handler
                handler = new LoadEventHandler(this);
                // Start the handler...
                handler.LoadDirectory(browser.SelectedPath, databaseIndex);
                // Keep a reference to the handler
                _loadEvents.Add(databaseIndex, handler); 
            }
        }

        #endregion // Methods
    }
}
