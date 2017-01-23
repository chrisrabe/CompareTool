using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.Display.Node
{
    public class DisplayNode : ObservableObject, IDisplayNode
    {
        private ObservableCollection<IDataNode> _dataNodes;

        public DisplayNode(IDatabase database)
        {
            _dataNodes = database.Data;
        }

        /// <summary>
        /// Gets or sets the data nodes of this display node.
        /// </summary>
        public ObservableCollection<IDataNode> DataNodes
        {
            get
            {
                if (_dataNodes == null)
                    _dataNodes = new ObservableCollection<IDataNode>();

                return _dataNodes;
            }
            set
            {
                if(_dataNodes != value)
                {
                    _dataNodes = value;
                    OnPropertyChanged("DataNodes");
                }
            }
        }

        /// <summary>
        /// Gets or sets the type of this display node.
        /// </summary>
        public string Type { get; set; }
    }
}
