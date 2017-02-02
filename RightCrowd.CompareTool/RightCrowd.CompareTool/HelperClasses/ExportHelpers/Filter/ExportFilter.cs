using System.Linq;
using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.Display.Data;
using RightCrowd.CompareTool.Models.Export.Data;
using RightCrowd.CompareTool.Models.Export.Node;
using RightCrowd.CompareTool.Models.Display.Node;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using RightCrowd.CompareTool.Models.Export.Configurations;

namespace RightCrowd.CompareTool.HelperClasses.ExportHelpers.Filter
{
    public class ExportFilter : IExportFilter
    {
        /// <summary>
        /// Creates a new export data which satisfies the preferred output.
        /// </summary>
        /// <param name="includeSimilarFields"></param>
        /// <param name="db1Only"></param>
        /// <param name="db2Only"></param>
        /// <param name="db1Results"></param>
        /// <param name="db2Results"></param>
        /// <returns></returns>
        public IExportData Filter(ExportConfiguration configuration, IDisplayData db1Results, IDisplayData db2Results)
        {
            if (db1Results == null || db2Results == null)
                return null;
            return new ExportData(configuration.DB2Only ? null : Filter(db1Results, configuration.IncludeSimilarFields), configuration.DB1Only ? null : Filter(db2Results, configuration.IncludeSimilarFields));
        }

        /// <summary>
        /// Iterates through all the differences in the given database comparison result.
        /// </summary>
        /// <param name="dbResults"></param>
        /// <param name="includeSimilarFields"></param>
        /// <returns></returns>
        private ObservableCollection<IExportNode> Filter(IDisplayData dbResults, bool includeSimilarFields)
        {
            ObservableCollection<IExportNode> tmp = new ObservableCollection<IExportNode>();
            dbResults.Differences.ToList().ForEach(dispNode =>
            {
                tmp.Add(new ExportNode(dispNode.Type, new ObservableCollection<IDataNode>(Filter(dispNode, includeSimilarFields))));
            });
            return tmp;
        }

        /// <summary>
        /// Returns a collection of data nodes. If the includeSimilarFields parameter is true, then
        /// it returns the data nodes inside the display node, otherwise it alters the data nodes to
        /// satisfy the preferred output.
        /// </summary>
        /// <param name="dispNode"></param>
        /// <param name="includeSimilarFields"></param>
        /// <returns></returns>
        private ObservableCollection<IDataNode> Filter(IDisplayNode dispNode, bool includeSimilarFields)
        {
            if (includeSimilarFields)
                return dispNode.DataNodes;
            else
            {
                return new ObservableCollection<IDataNode>(dispNode.DataNodes.Select(GetDifferentFieldsOnly));
            }
        }

        /// <summary>
        /// Creates a new data node which contains only different fields.
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        private IDataNode GetDifferentFieldsOnly(IDataNode node)
        {
            return new DataNode(node.FileName, new ObservableCollection<IField>(node.Fields.Where(field => field.Different)));
        }
    }
}
