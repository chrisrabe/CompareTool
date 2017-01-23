using System;
using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.Display.Node;
using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.Display.Data
{
    public class DisplayData : ObservableObject, IDisplayData
    {
        private ObservableCollection<IDisplayNode> _displayNodes;

        public DisplayData(ObservableCollection<IDisplayNode> displayNodes)
        {
            DisplayNodes = displayNodes;
        }

        public ObservableCollection<IDisplayNode> DisplayNodes
        {
            get
            {
                if (_displayNodes == null)
                    _displayNodes = new ObservableCollection<IDisplayNode>();

                return _displayNodes;
            }

            set
            {
                if(_displayNodes != value)
                {
                    _displayNodes = value;
                    OnPropertyChanged("DisplayNodes");
                }
            }
        }
    }
}
