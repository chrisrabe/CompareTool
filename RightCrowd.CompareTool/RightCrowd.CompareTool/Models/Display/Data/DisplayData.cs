using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.Display.Node;
using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.Display.Data
{
    public class DisplayData : ObservableObject, IDisplayData
    {
        private ObservableCollection<IDisplayNode> _differences;
        private ObservableCollection<IDisplayNode> _similarities;

        public DisplayData(ObservableCollection<IDisplayNode> differences, ObservableCollection<IDisplayNode> similarities)
        {
            Differences = differences;
            Similarities = similarities;
        }

        public ObservableCollection<IDisplayNode> Differences
        {
            get
            {
                if (_differences == null)
                    Differences = new ObservableCollection<IDisplayNode>();

                return _differences;
            }

            set
            {
                if(_differences != value)
                {
                    _differences = value;
                    OnPropertyChanged("Differences");
                }
            }
        }

        public ObservableCollection<IDisplayNode> Similarities
        {
            get
            {
                if (_similarities == null)
                    Similarities = new ObservableCollection<IDisplayNode>();
                return _similarities;
            }
            set
            {
                if(_similarities != value)
                {
                    _similarities = value;
                    OnPropertyChanged("Similarities");
                }
            }
        }
    }
}
