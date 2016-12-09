using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Input;

namespace RightCrowd.CompareTool
{
    /// <summary>
    /// This class is responsible for changing the view of the main window.
    /// </summary>
    public class ApplicationViewModel : ObservableObject
    {

        #region Fields

        private static ApplicationViewModel _instance;

        private IDatabaseStorage _databaseStorage;

        private ICommand _changePageCommand;

        private IPageViewModel _currentPageViewModel;
        private List<IPageViewModel> _pageViewModels;

        #endregion // Fields

        #region Constructors

        /// <summary>
        /// Creates a new instance of the 
        /// application view model if an instance is not yet created.
        /// </summary>
        public static ApplicationViewModel Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new ApplicationViewModel();

                return _instance;
            }
        }

        private ApplicationViewModel()
        {
            // Add available pages
            PageViewModels.Add(new LoadViewModel());

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        #endregion // Constructors

        #region Properties

        /// <summary>
        /// Returns an instance of a database storage.
        /// </summary>
        public IDatabaseStorage DatabaseStorage
        {
            get
            {
                if (_databaseStorage == null)
                    _databaseStorage = new TwoDatabaseStorage();

                return _databaseStorage;
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
