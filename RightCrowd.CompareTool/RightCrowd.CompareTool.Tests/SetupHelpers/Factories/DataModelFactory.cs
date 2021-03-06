﻿using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using System.Collections.ObjectModel;
using System.Linq;

namespace RightCrowd.CompareTool.Tests.SetupHelpers.Factories
{
    /// <summary>
    /// This class is responsible for creating the data models.
    /// </summary>
    internal class DataModelFactory
    {
        /// <summary>
        /// Constructs a database given the name and the data nodes.
        /// </summary>
        /// <param name="databaseName"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        internal IDatabase CreateDatabase(string databaseName, ObservableCollection<IDataNode> data)
        {
            IDatabase database =  new Database(databaseName);
            database.Data = data;
            return database;
        }

        /// <summary>
        /// Constructs the data node given the node name and the fields.
        /// </summary>
        /// <param name="nodeName"></param>
        /// <param name="fields"></param>
        /// <returns></returns>
        internal IDataNode CreateNode(string nodeName, ObservableCollection<IField> fields)
        {
            return new DataNode(nodeName, fields);
        }

        /// <summary>
        /// Creates the composite field containing passed children.
        /// </summary>
        /// <param name="fieldname"></param>
        /// <param name="children"></param>
        /// <returns></returns>
        internal IField CreateCompositeField(string fieldname, params IField[] children)
        {
            CompositeField composite = new CompositeField(fieldname);
            children.ToList().ForEach(child => composite.Fields.Add(child));
            return composite;
        }

        /// <summary>
        /// Creates a raw field.
        /// </summary>
        /// <param name="fieldname"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        internal IField CreateRawField(string fieldname, string value)
        {
            return new RawField(fieldname, value);
        }
    }
}
