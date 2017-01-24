using RightCrowd.CompareTool.HelperClasses;
using RightCrowd.CompareTool.Models.Display.Node;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool
{
    public class CompareViewModel : ObservableObject, IPageViewModel
    {
        #region Fields

        private ObservableCollection<IDisplayNode> _db1Differences;
        private ObservableCollection<IDisplayNode> _db1Similarities;


        private ObservableCollection<IDisplayNode> _db2Differences;
        private ObservableCollection<IDisplayNode> _db2Similarities;

        #endregion // Fields

        #region Properties

        public ObservableCollection<IDisplayNode> DB1Differences
        {
            get
            {
                if (_db1Differences == null)
                    _db1Differences = new ObservableCollection<IDisplayNode>();

                return _db1Differences;
            }
            set
            {
                if (_db1Differences != value)
                {
                    _db1Differences = value;
                    OnPropertyChanged("DB1Differences");
                }
            }
        }

        public ObservableCollection<IDisplayNode> DB1Similarities
        {
            get
            {
                if (_db1Similarities == null)
                    _db1Similarities = new ObservableCollection<IDisplayNode>();

                return _db1Similarities;
            }
            set
            {
                if (_db1Similarities != value)
                {
                    _db1Similarities = value;
                    OnPropertyChanged("DB1Similarities");
                }
            }
        }
        public ObservableCollection<IDisplayNode> DB2Differences
        {
            get
            {
                if (_db2Differences == null)
                    _db2Differences = new ObservableCollection<IDisplayNode>();

                return _db2Differences;
            }
            set
            {
                if (_db2Differences != value)
                {
                    _db2Differences = value;
                    OnPropertyChanged("DB2Differences");
                }
            }
        }


        public ObservableCollection<IDisplayNode> DB2Similarities
        {
            get
            {
                if (_db2Similarities == null)
                    _db2Similarities = new ObservableCollection<IDisplayNode>();

                return _db2Similarities;
            }
            set
            {
                if (_db2Similarities != value)
                {
                    _db2Similarities = value;
                    OnPropertyChanged("DB2Similarities");
                }
            }
        }

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
