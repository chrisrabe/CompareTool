
using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool
{
    public class ExportViewModel : ObservableObject, IPageViewModel
    {
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
