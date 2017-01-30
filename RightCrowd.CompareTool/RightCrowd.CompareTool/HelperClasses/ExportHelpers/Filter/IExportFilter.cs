using RightCrowd.CompareTool.Models.Display.Data;
using RightCrowd.CompareTool.Models.Export.Configurations;
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
        /// <param name="configuration"></param>
        /// <param name="db1Results"></param>
        /// <param name="db2Results"></param>
        /// <returns></returns>
        IExportData Filter(ExportConfiguration configuration, IDisplayData db1Results, IDisplayData db2Results);
    }
}
