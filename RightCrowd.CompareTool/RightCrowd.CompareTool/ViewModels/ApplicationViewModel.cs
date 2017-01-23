using System.Linq;
using System.Windows.Input;
using System.Collections.Generic;
using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;
using RightCrowd.CompareTool.HelperClasses.Providers.Database;
using RightCrowd.CompareTool.HelperClasses.Providers.CompareData;

namespace RightCrowd.CompareTool
{
    /// <summary>
    /// This class is responsible for changing the view of the main window.
    /// </summary>
    public class ApplicationViewModel : ObservableObject
    {

        #region Fields

        private IDatabaseStorageProvider _databaseProvider; 
        private ICompareDataProvider _compareDataProvider;

        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        #endregion // Fields

        #region Constructors

        public ApplicationViewModel()
        {
            // Initialise Fields
            _databaseProvider = new DatabaseStorageProvider();
            _compareDataProvider = new CompareDataProvider();

            // Add available pages
            PageViewModels.Add(new LoadViewModel(_databaseProvider, _compareDataProvider));
            PageViewModels.Add(new CompareViewModel(_compareDataProvider));
            

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Returns an instance of a database storage.
        /// </summary>
        public IListDatabaseStorage DatabaseStorage
        {
            get
            {
                return _databaseProvider.DatabaseStorage;
            }
        }

        public IComparisonDataStorage ComparisonStorage
        {
            get { return _compareDataProvider.ComparisonStorage; }
            set
            {
                _compareDataProvider.ComparisonStorage = value;
            }
        }

        public IPageViewModel CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }

            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }
        
        public List<IPageViewModel> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<IPageViewModel>();

                return _pageViewModels;
            }
        }

        #endregion // Properties

        #region  Commands

        public ICommand ChangePageCommand
        {
            get
            {
                if(_changePageCommand == null)
                {
                    _changePageCommand = new RelayCommand(
                        p => ChangeViewModel((IPageViewModel)p),
                        p => p is IPageViewModel);
                }

                return _changePageCommand;
            }
        }

        #endregion // Commands

        #region Methods

        private void ChangeViewModel(IPageViewModel viewModel)
        {
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion // Methods
    }
}
