using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.HelperClasses.Providers.DisplayData;

namespace RightCrowd.CompareTool
{
    public class ExportViewModel : ObservableObject, IPageViewModel
    {
        private IDisplayDataProvider _displayDataProvider;

        public ExportViewModel(IDisplayDataProvider displayDataProvider)
        {
            _displayDataProvider = displayDataProvider;
        }

        #region IPageViewModel Members

        public string ImagePath
        {
            get
            {
                return "pack://application:,,,/Resources/save_icon.png";
            }
        }

        public string Name
        {
            get
            {
                return "Export Results";
            }
        }

        #endregion // IPageViewmodel Members
    }
}
