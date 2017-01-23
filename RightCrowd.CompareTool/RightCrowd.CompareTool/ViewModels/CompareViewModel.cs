using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.HelperClasses.Providers.CompareData;

namespace RightCrowd.CompareTool
{
    public class CompareViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private ICompareDataProvider _compareDataProvider;

        #endregion // Fields

        #region Constructors

        public CompareViewModel(ICompareDataProvider compareDataProvider)
        {
            _compareDataProvider = compareDataProvider;
        }

        #endregion // Constructors

        #region Properties
        


        #endregion // Properties

        #region IPageViewModel Members

        public string ImagePath
        {
            get
            {
                return "pack://application:,,,/Resources/compare-btn.png";
            }
        }

        public string Name
        {
            get
            {
                return "Comparison Result";
            }
        }

        #endregion // IPageViewModel Members
    }
}
