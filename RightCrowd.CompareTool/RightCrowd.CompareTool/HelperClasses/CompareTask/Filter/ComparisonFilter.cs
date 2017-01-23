using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;
using RightCrowd.CompareTool.Models.Display.Data;
using RightCrowd.CompareTool.Models.Display.Node;
using RightCrowd.CompareTool.Models.Display.Partition;
using System.Collections.ObjectModel;
using System.Linq;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Filter
{
    internal class ComparisonFilter : IComparisonFilter
    {
        public IDisplayPartition FilterStorage(int database, IComparisonDataStorage storage)
        {
            IDisplayData difference = FilterData(database, true, storage);
            IDisplayData similarities = FilterData(database, false, storage);
            return new DisplayPartition(difference, similarities);
        }

        private IDisplayData FilterData(int database, bool parseDifference, IComparisonDataStorage storage)
        {
            ObservableCollection<IDisplayNode> nodes = new ObservableCollection<IDisplayNode>();
            storage.ComparisonData.ToList().ForEach(data =>
            {
                IListDatabaseStorage dbStorage = parseDifference ? data.Difference : data.Similarities;
                IDatabase db = dbStorage[database];
                if(db != null && db.Data.Count > 0)
                    nodes.Add(new DisplayNode(data.Type, db.Data));
            });
            return new DisplayData(nodes);
        }
    }
}
