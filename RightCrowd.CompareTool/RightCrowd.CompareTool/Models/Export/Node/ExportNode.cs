using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.DataModels.DataNode;

namespace RightCrowd.CompareTool.Models.Export.Node
{
    public class ExportNode : IExportNode
    {
        private ObservableCollection<IDataNode> _dataNodes;

        public ExportNode(string type, ObservableCollection<IDataNode> dataNodes)
        {
            Type = type;
            DataNodes = dataNodes;
        }

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
                if (_dataNodes != value)
                    _dataNodes = value;
            }
        }

        public string Type { get; set; }
    }
}
