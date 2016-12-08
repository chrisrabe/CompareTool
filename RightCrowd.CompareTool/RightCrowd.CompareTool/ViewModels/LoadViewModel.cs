using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool
{
    public class LoadViewModel : ObservableObject, IPageViewModel
    {
        public string ImagePath
        {
            get { return "pack://application:,,,/Resources/load_view-logo.png"; }
        }

        public string Name
        {
            get
            {
                return "Load Databases.";
            }
        }
    }
}
