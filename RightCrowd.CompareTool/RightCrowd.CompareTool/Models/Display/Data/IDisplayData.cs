using RightCrowd.CompareTool.Models.Display.Node;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool.Models.Display.Data
{
    /// <summary>
    /// Stores one type of result from the comparison data. 
    /// In other words, it can either store all the differences
    /// and similarities of a single database.
    /// </summary>
    public interface IDisplayData
    {
        /// <summary>
        /// Gets or sets the collection of differences between the databases.
        /// </summary>
        /// 
        ObservableCollection<IDisplayNode> Differences { get; set; }

        /// <summary>
        /// Gets or sets the collection of similarities between the databases
        /// </summary>
        ObservableCollection<IDisplayNode> Similarities { get; set; }
    }
}
