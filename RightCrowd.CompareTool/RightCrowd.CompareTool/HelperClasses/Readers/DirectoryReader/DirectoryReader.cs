using System;
using RightCrowd.CompareTool.Models.DataModels.Database;
using System.IO;
using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses.Readers.DirectoryReader
{
    internal class DirectoryReader : IDirectoryReader
    {
        /// <summary>
        /// Reads each file of the directory and passes it down to the XML Reader.
        /// This requires all files in the directory to be XML files.
        /// </summary>
        /// <param name="directoryPath"></param>
        /// <returns></returns>
        public IDatabase ReadDirectory(string directoryPath)
        {
            string[] files = Directory.GetFiles(directoryPath);
            foreach(string file in files)
            {
                // DO stuff
            }
            return null;
        }
    }
}
