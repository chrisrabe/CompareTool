using RightCrowd.CompareTool.Models.Display.Data;
using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.Display.Partition
{
    public class DisplayPartition : ObservableObject, IDisplayPartition
    {
        private IDisplayData _differences;
        private IDisplayData _similarities;

        public DisplayPartition(IDisplayData differences, IDisplayData similarities)
        {
            Differences = differences;
            Similarities = similarities;
        }

        public IDisplayData Differences
        {
            get
            {
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

        public IDisplayData Similarities
        {
            get
            {
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
