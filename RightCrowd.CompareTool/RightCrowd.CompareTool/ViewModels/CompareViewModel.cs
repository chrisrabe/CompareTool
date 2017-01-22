using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool
{
    public class CompareViewModel : ObservableObject, IPageViewModel
    {
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
