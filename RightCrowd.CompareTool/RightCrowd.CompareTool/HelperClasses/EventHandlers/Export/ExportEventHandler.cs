using RightCrowd.CompareTool.HelperClasses.ExportHelpers.FileConverter;
using RightCrowd.CompareTool.HelperClasses.ExportHelpers.Filter;
using RightCrowd.CompareTool.HelperClasses.Providers.DisplayData;
using RightCrowd.CompareTool.Models.Export.Configurations;
using RightCrowd.CompareTool.Models.Export.Data;
using System.Windows;

namespace RightCrowd.CompareTool.HelperClasses.EventHandlers.Export
{
    /// <summary>
    /// This class handles all export events from the view.
    /// </summary>
    public class ExportEventHandler
    {
        private IExportFilter _filter;
        private IFileConverter _converter;
        private IDisplayDataProvider _provider;
        private ExportConfiguration _configuration;

        public ExportEventHandler(IExportFilter filter, IFileConverter converter, ExportConfiguration configuration, IDisplayDataProvider provider)
        {
            _filter = filter;
            _converter = converter;
            _provider = provider;
            _configuration = configuration;
        }

        /// <summary>
        /// Exports the constructor arguments to produce an output.
        /// - The provider contains the comparison data
        /// - The filter ensures that the conditions inside the export configuration is met by the output.
        /// - The file converter transforms the export data into a specific file format.
        /// </summary>
        /// <param name="filepath"></param>
        public void Export(string filePath)
        {
            IExportData data = _filter.Filter(_configuration, _provider.DB1Results, _provider.DB2Results);
            _converter.ToFile(filePath, data);
            MessageBox.Show("Exported to " + filePath);
        }
    }
}
