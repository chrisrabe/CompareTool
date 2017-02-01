using RightCrowd.CompareTool.HelperClasses;

namespace RightCrowd.CompareTool.Models.Export.Configurations
{
    /// <summary>
    /// This class contains properties which determines the output when
    /// exporting the results of the comparison data.
    /// </summary>
    public class ExportConfiguration
    {

        public ExportConfiguration(bool includeSimilarFields, bool db1Only, bool db2Only)
        {
            IncludeSimilarFields = includeSimilarFields;
            DB1Only = db1Only;
            DB2Only = db2Only;
        }

        #region Properties

        /// <summary>
        /// If true, it should export all the data under the differences tab;
        /// including the fields which are similar to the other database.
        /// If false, it should only export the fields which are marked
        /// different.
        /// </summary>
        public bool IncludeSimilarFields { get; set; }
        /// <summary>
        /// If true, it should export only the comparison results for database
        /// one.
        /// </summary>
        public bool DB1Only { get; set; }

        /// <summary>
        /// If true, it should only export the comparison results for database
        /// two.
        /// </summary>
        public bool DB2Only { get; set; }

        #endregion // Properties
    }
}
