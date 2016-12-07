using RightCrowd.CompareTool.Models.DataModels.DataNode;
using System.Collections.ObjectModel;

namespace RightCrowd.CompareTool.Models.DataModels.Database
{
    public interface IDatabase
    {
        ObservableCollection<IDataNode> Data { get; set; }

        string DirectoryName { get; set; }
    }
}
