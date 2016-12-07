using RightCrowd.CompareTool.Models.DataModels.Fields;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool.Models.DataModels.DataNode
{
    public interface IDataNode
    {
        /// <summary>
        /// Gets or sets the type of the data node.
        /// </summary>
        string Type { get;}

        /// <summary>
        /// Gets or sets the fields related to this data node.
        /// </summary>
        ObservableCollection<IField> Fields{ get;}
    }
}
