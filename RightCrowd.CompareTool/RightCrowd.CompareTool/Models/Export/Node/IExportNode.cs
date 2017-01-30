using RightCrowd.CompareTool.Models.DataModels.DataNode;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool.Models.Export.Node
{
    /// <summary>
    /// Contains a collection of data nodes with a specific type.
    /// </summary>
    public interface IExportNode
    {
        string Type { get; set; }

        ObservableCollection<IDataNode> DataNodes { get; set; }
    }
}
