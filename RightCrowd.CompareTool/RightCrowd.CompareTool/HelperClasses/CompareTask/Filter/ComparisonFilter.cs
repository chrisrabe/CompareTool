using RightCrowd.CompareTool.Models.Comparison.DataStorage;
using RightCrowd.CompareTool.Models.Display.Data;
using RightCrowd.CompareTool.Models.Display.Partition;

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
            return null;
        }
    }
}
