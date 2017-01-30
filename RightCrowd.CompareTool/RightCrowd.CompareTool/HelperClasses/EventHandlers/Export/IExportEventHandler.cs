using RightCrowd.CompareTool.Models.Display.Data;

namespace RightCrowd.CompareTool.HelperClasses.EventHandlers.Export
{
    /// <summary>
    /// This interface provides a method contract for an export event handler.
    /// </summary>
    public interface IExportEventHandler
    {
        /// <summary>
        /// Exports the two display data into a specific type of file.
        /// 
        /// Export Operations range from 1 - 3.
        /// 1 - Export All : includes similarities and differences
        /// 2 - Export Differences Only : Exports the differences for each database. Includes fields which are not different.
        /// 3 - Export Different Fields Only : Exports the differences for each databases. Only exports fields which are different.
        /// 
        /// </summary>
        /// <param name="exportOperation"></param>
        /// <param name="db1Only"></param>
        /// <param name="db2Only"></param>
        /// <param name="db1Results"></param>
        /// <param name="db2Results"></param>
        void Export(int exportOperation, bool db1Only, bool db2Only, IDisplayData db1Results, IDisplayData db2Results);
    }
}
