using RightCrowd.CompareTool.Models.Display.Data;
using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.Display.Partition
{
    public class DisplayPartition : ObservableObject, IDisplayPartitions
    {
        private string _directory;
        private IDisplayData _differences;
        private IDisplayData _similarities;

        public DisplayPartition(string directory, IDisplayData differences, IDisplayData similarities)
        {
            Directory = directory;
            Differences = differences;
            Similarities = similarities;
        }

        public string Directory
        {
            get
            {
                return _directory;
            }

            set
            {
                if(_directory != value)
                {
                    _directory = value;
                    OnPropertyChanged("Directory");
                }
            }
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
