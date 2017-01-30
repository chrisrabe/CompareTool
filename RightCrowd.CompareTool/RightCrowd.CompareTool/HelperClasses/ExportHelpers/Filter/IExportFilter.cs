using RightCrowd.CompareTool.Models.Display.Data;
using RightCrowd.CompareTool.Models.Export.Data;

namespace RightCrowd.CompareTool.HelperClasses.ExportHelpers.Filter
{
    /// <summary>
    ///  This interface provides method contracts for an export filter.
    /// </summary>
    public interface IExportFilter
    {
        /// <summary>
        /// Creates an export data which satisfies the configurations set by the user.
        /// </summary>
        /// <param name="includeSimilarFields"></param>
        /// <param name="db1Only"></param>
        /// <param name="db2Only"></param>
        /// <param name="db1Results"></param>
        /// <param name="db2Results"></param>
        /// <returns></returns>
        IExportData Filter(bool includeSimilarFields, bool db1Only, bool db2Only, IDisplayData db1Results, IDisplayData db2Results);
    }
}
