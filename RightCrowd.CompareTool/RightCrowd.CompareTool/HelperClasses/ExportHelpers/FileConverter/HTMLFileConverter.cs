using RightCrowd.CompareTool.Models.Export.Data;
using System.IO;
using System.Text;
using System;
using RightCrowd.CompareTool.Models.Export.Node;
using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using System.Linq;

namespace RightCrowd.CompareTool.HelperClasses.ExportHelpers.FileConverter
{
    public class HTMLFileConverter : IFileConverter
    {
        public void ToFile(string filePath, IExportData data)
        {
            using (StreamWriter outputFile = new StreamWriter(filePath))
            {
                outputFile.WriteLine(@"<html>");
                // Apppend the header content
                AppendHeader(outputFile);
                // Append the data
                AppendBody(outputFile, data);
                outputFile.WriteLine(@"</html>");
            }
        }

        #region Export Data parsing methods

        private void AppendBody(StreamWriter outputFile, IExportData data)
        {
            outputFile.WriteLine(CreateTabs(1) + @"<body>");
            CreateTable(outputFile, data, 2);
            outputFile.WriteLine(CreateTabs(1) + @"</body>");
        }

        private void CreateTable(StreamWriter outputFile, IExportData data, int level)
        {
            outputFile.WriteLine(CreateTabs(level) + "<table style=\"width:100%\">");
            CreateTableRows(outputFile, data, level + 1);
            outputFile.WriteLine(CreateTabs(level) + @"</table>");
        }

        private void CreateTableRows(StreamWriter outputFile, IExportData data, int level)
        {
            // First create the table headers 
            outputFile.WriteLine(CreateTabs(level) + @"<tr>");
            if (data.DatabaseOneData.Count > 0)
                outputFile.WriteLine(CreateTabs(level + 1) + @"<th>Database One Differences</th>");
            if (data.DatabaseTwoData.Count > 0)
                outputFile.WriteLine(CreateTabs(level + 1) + @"<th>Database Two Differences</th>");
            outputFile.WriteLine(CreateTabs(level) + @"</tr>");
            // Now parse the data
            outputFile.WriteLine(CreateTabs(level) + @"<tr>");
            if (data.DatabaseOneData.Count > 0)
                CreateTableData(outputFile, data.DatabaseOneData, level + 1);
            if (data.DatabaseTwoData.Count > 0)
                CreateTableData(outputFile, data.DatabaseTwoData, level + 1);
            outputFile.WriteLine(CreateTabs(level) + @"</tr>");
        }

        private void CreateTableData(StreamWriter outputFile, ObservableCollection<IExportNode> data, int level)
        {
            outputFile.WriteLine(CreateTabs(level) + @"<td>");
            WriteDataToFile(outputFile, data, level + 1);
            outputFile.WriteLine(CreateTabs(level) + @"</td>");
        }

        private void WriteDataToFile(StreamWriter outputFile, ObservableCollection<IExportNode> data, int level)
        {
            outputFile.WriteLine(CreateTabs(level) + "<table style=\"width:100%\">");
            // Create the row with headers
            outputFile.WriteLine(CreateTabs(level + 1) + "<tr>");
            outputFile.WriteLine(CreateTabs(level + 2) + "<th>Type</th>");
            outputFile.WriteLine(CreateTabs(level + 2) + "<th>Data</th>");
            outputFile.WriteLine(CreateTabs(level + 1) + "</tr>");
            // Parse the data
            data.ToList().ForEach(node => WriteToFile(outputFile, node, level + 1));
            outputFile.WriteLine(CreateTabs(level) + @"</table>");
        }

        private void WriteToFile(StreamWriter outputFile, IExportNode node, int level)
        {
            outputFile.WriteLine(CreateTabs(level) + "<tr>");
            outputFile.WriteLine(CreateTabs(level + 1) + $"<td>{node.Type}</td>");
            // Parse the nodes
            outputFile.WriteLine(CreateTabs(level + 1) + "<td>");
            outputFile.WriteLine(CreateTabs(level + 2) + "<ul>");
            node.DataNodes.ToList().ForEach(data => WriteToFile(outputFile, data, level + 3));
            outputFile.WriteLine(CreateTabs(level + 2) + "</ul>");
            outputFile.WriteLine(CreateTabs(level + 1) + "</td>");
            outputFile.WriteLine(CreateTabs(level) + "</tr>");
        }

        private void WriteToFile(StreamWriter outputFile, IDataNode data, int level)
        {
            outputFile.WriteLine(CreateTabs(level) + @"<li>");
            outputFile.WriteLine(CreateTabs(level + 1) + data.FileName);
            // Write the fields
            outputFile.WriteLine(CreateTabs(level + 1) + @"<ul>");
            data.Fields.ToList().ForEach(field => WriteToFile(outputFile, field, level + 2));
            outputFile.WriteLine(CreateTabs(level + 1) + @"</ul>");
            outputFile.WriteLine(CreateTabs(level) + @"</li>");
        }

        private void WriteToFile(StreamWriter outputFile, IField field, int level)
        {
            if (field is RawField)
                outputFile.WriteLine(CreateTabs(level) + $"<li>{field.ToString()}</li>");
            else
            {
                outputFile.WriteLine(CreateTabs(level) + @"<li>");
                outputFile.WriteLine(CreateTabs(level + 1) + field.Name);
                outputFile.WriteLine(CreateTabs(level + 1) + @"<ul>");
                ((CompositeField)field).Fields.ToList().ForEach(child => WriteToFile(outputFile, child, level + 2));
                outputFile.WriteLine(CreateTabs(level + 1) + @"</ul>");
                outputFile.WriteLine(CreateTabs(level) + @"</li>");
            }
        }

        #endregion // Export Data parsing methods

        #region Helper Methods

        /// <summary>
        /// Appends a string which follows this syntax:
        /// 
        /// <head>
        ///     <style>
        ///         table, th, td {
        ///             border: 1px solid black;
        ///             border-collapse: collapse;
        ///             padding: 5px;
        ///             vertical-align:top;
        ///         }
        ///     </style>
        ///</head>
        /// 
        /// to the output file.
        /// 
        /// </summary>
        /// <param name="outputFile"></param>
        private void AppendHeader(StreamWriter outputFile)
        {
            outputFile.WriteLine(CreateTabs(1) + @"<head>");
            outputFile.WriteLine(CreateTabs(2) + @"<style>");
            outputFile.WriteLine(CreateTabs(3) + @"table, th, td {");
            outputFile.WriteLine(CreateTabs(4) + @"border : 1px solid black;");
            outputFile.WriteLine(CreateTabs(4) + @"border-collapse : collapse");
            outputFile.WriteLine(CreateTabs(4) + @"padding: 5px;");
            outputFile.WriteLine(CreateTabs(4) + @"vertical-align:top;");
            outputFile.WriteLine(CreateTabs(3) + @"}");
            outputFile.WriteLine(CreateTabs(2) + @"</style>");
            outputFile.WriteLine(CreateTabs(1) + @"</head>");
        }

        public string CreateTabs(int level)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < level; i++)
                sb.Append("\t");
            return sb.ToString();
        }

        #endregion // Helper methods
    }
}
