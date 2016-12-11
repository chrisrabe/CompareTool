using System;
using RightCrowd.CompareTool.Models.DataModels.Database;

namespace RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List
{
    public interface IListDatabaseStorage : IDatabaseStorage
    {
        
        /// <summary>
        /// Gets or sets the database at the chosen index.
        /// </summary>
        /// <param name="index"></param>
        /// <returns></returns>
        IDatabase this[int index] { get; set; }
    }
}
