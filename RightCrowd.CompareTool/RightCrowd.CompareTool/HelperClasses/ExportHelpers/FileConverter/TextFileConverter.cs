using System.IO;
using System.Linq;
using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.Export.Data;
using RightCrowd.CompareTool.Models.Export.Node;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using System.Text;

namespace RightCrowd.CompareTool.HelperClasses.ExportHelpers.FileConverter
{
    /// <summary>
    /// Converts the export data into a text file.
    /// </summary>
    public class TextFileConverter : IFileConverter
    {
        public void ToFile(string filePath, IExportData data)
        {
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                if (data.DatabaseOneData.Count > 0)
                    WriteToFile(outputFile, data.DatabaseOneData, "Database One Differences");
                if (data.DatabaseTwoData.Count > 0)
                    WriteToFile(outputFile, data.DatabaseTwoData, "Database Two Differences");
            }
        }

        #region Parsing Methods

        private void WriteToFile(StreamWriter outputFile, ObservableCollection<IExportNode> data, string header)
        {
            outputFile.WriteLine(header);
            data.ToList().ForEach(node => WriteToFile(outputFile, node));
        }

        private void WriteToFile(StreamWriter outputFile, IExportNode node)
        {
            outputFile.WriteLine(node.Type);
            node.DataNodes.ToList().ForEach(elem => WriteToFile(outputFile, elem, 1));
        }

        private void WriteToFile(StreamWriter outputFile, IDataNode node, int level)
        {
            outputFile.WriteLine(CreateTabs(level) + node.FileName);
            node.Fields.ToList().ForEach(field => WriteToFile(outputFile, field, level + 1));
        }

        private void WriteToFile(StreamWriter outputFile, IField field, int level)
        {
            if (field is RawField)
                outputFile.WriteLine(CreateTabs(level) + field.ToString());
            else // field is a composite field
            {
                outputFile.WriteLine(CreateTabs(level) + field.Name);
                ((CompositeField)field).Fields.ToList().ForEach(child => WriteToFile(outputFile, child, level + 1));
            }
        }

        #endregion // Parsing Methods

        #region Helper Methods

        private string CreateTabs(int level)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < level; i++)
                sb.Append("\t");
            return sb.ToString();
        }

        #endregion // Helper Methods
    }
}
