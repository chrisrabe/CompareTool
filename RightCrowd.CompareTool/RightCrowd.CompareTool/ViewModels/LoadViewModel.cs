using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage;

namespace RightCrowd.CompareTool
{
    public class LoadViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private IDatabaseStorage _databaseStorage;

        // Progress bar fields
        private int _loadDB1Progress;
        private int _loadDB2Progress;
        private int _compareProgress;

        #endregion // Fields

        #region Constructors

        public LoadViewModel()
        {
            _databaseStorage = ApplicationViewModel.Instance.DatabaseStorage;
        }

        #endregion // Constructors

        #region ICommands



        #endregion // ICommands

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
            get { return _loadDB1Progress; }
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
    }
}
