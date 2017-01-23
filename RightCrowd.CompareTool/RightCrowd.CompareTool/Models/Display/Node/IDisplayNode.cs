using RightCrowd.CompareTool.Models.DataModels.DataNode;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool.Models.Display.Node
{
    /// <summary>
    /// Stores a collection of IDataNodes with similar types.
    /// </summary>
    public interface IDisplayNode
    {
        /// <summary>
        /// Gets or sets the type of this display node
        /// </summary>
        string Type { get; set; }

        /// <summary>
        /// Gets or sets the data nodes of this display node
        /// </summary>
        ObservableCollection<IDataNode> DataNodes { get; set; }
    }
}
