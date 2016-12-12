using RightCrowd.CompareTool.Models.DataModels.Fields;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool.Models.DataModels.DataNode
{
    public interface IDataNode
    {
        /// <summary>
        /// Gets or sets the type of the data node.
        /// </summary>
        string FileName { get;}

        /// <summary>
        /// Gets or sets the fields related to this data node.
        /// </summary>
        ObservableCollection<IField> Fields{ get;}

        /// <summary>
        /// This returns a boolean flag which indicates that this
        /// node does not exist in the other database. The field
        /// is mainly used in comparison logic.
        /// </summary>
        bool Different { get; set; }
    }
}
