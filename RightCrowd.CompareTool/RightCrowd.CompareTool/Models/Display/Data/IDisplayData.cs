using RightCrowd.CompareTool.Models.Display.Node;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool.Models.Display.Data
{
    /// <summary>
    /// Stores one type of result from the comparison data. 
    /// In other words, it can either store all the differences
    /// or similarities of a single database.
    /// </summary>
    public interface IDisplayData
    {
        /// <summary>
        /// Gets or sets the collection of display nodes for this display data.
        /// </summary>
        /// 
        ObservableCollection<IDisplayNode> DisplayNodes { get; set; }
    }
}
