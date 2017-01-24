using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;
using RightCrowd.CompareTool.Models.Display.Data;
using RightCrowd.CompareTool.Models.Display.Node;
using System.Collections.ObjectModel;
using System.Linq;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Filter
{
    /// <summary>
    /// This class is responsible for filtering the comparison results.
    /// </summary>
    internal class ComparisonFilter : IComparisonFilter
    {

        /// <summary>
        /// This method removes all the redundant data, such as arrays which contains zero elements.
        /// </summary>
        /// <param name="database"></param>
        /// <param name="storage"></param>
        /// <returns></returns>
        public IDisplayData FilterStorage(int database, IComparisonDataStorage storage)
        {
            return new DisplayData(FilterData(database, true, storage), FilterData(database, false, storage));
        }

        private ObservableCollection<IDisplayNode> FilterData(int database, bool parseDifference, IComparisonDataStorage storage)
        {
            ObservableCollection<IDisplayNode> nodes = new ObservableCollection<IDisplayNode>();
            storage.ComparisonData.ToList().ForEach(data =>
            {
                IListDatabaseStorage dbStorage = parseDifference ? data.Difference : data.Similarities;
                IDatabase db = dbStorage[database];
                if (db != null && db.Data.Count > 0)
                    nodes.Add(new DisplayNode(data.Type, db.Data));
            });
            return nodes;
        }
    }
}
