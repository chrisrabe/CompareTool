using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using RightCrowd.CompareTool.Tests.SetupHelpers.Factories;
using System.Collections.ObjectModel;
using System;
using System.Linq;

namespace RightCrowd.CompareTool.Tests.SetupHelpers.TestSetup
{
    /// <summary>
    /// This class is responsible for creating mock databases for the compare test.
    /// </summary>
    internal class CompareTestSetup
    {
        #region Fields

        private DataModelFactory _factory;

        private int _DB1expectedDifferences;
        private int _DB1expectedSimilarities;
        private int _DB1expectedFieldDifference;
        private int _DB1NumberOfFields;

        private int _DB2expectedDifferences;
        private int _DB2expectedSimilarities;
        private int _DB2expectedFieldDifference;
        private int _DB2NumberOfFields;

        private int _numCompositeFields; // number of composite fields in each node
        private int _numCompositeChild; // number of nested fields in each composite field

        #endregion // Fields

        #region Constructors

        public CompareTestSetup()
        {
            _factory = new DataModelFactory();
        }

        #endregion // Constructors

        #region Database One Properties


        public int DB1NumberOfFields
        {
            get { return _DB1NumberOfFields; }
            set
            {
                if (_DB1NumberOfFields != value)
                    _DB1NumberOfFields = value;
            }
        }

        public int DB1ExpectedFieldDifference
        {
            get { return _DB1expectedFieldDifference; }
            set
            {
                if (_DB1expectedFieldDifference != value)
                    _DB1expectedFieldDifference = value;
            }
        }

        public int DB1ExpectedDifferences
        {
            get { return _DB1expectedDifferences; }
            set
            {
                if (_DB1expectedDifferences != value)
                    _DB1expectedDifferences = value;
            }
        }

        public int DB1ExpectedSimilarities
        {
            get { return _DB1expectedSimilarities; }
            set
            {
                if (_DB1expectedSimilarities != value)
                    _DB1expectedSimilarities = value;
            }
        }

        #endregion // Database One Properties

        #region Database Two Properties

        public int DB2NumberOfFields
        {
            get { return _DB2NumberOfFields; }
            set
            {
                if (_DB2NumberOfFields != value)
                    _DB2NumberOfFields = value;
            }
        }

        public int DB2ExpectedFieldDifference
        {
            get { return _DB2expectedFieldDifference; }
            set
            {
                if (_DB2expectedFieldDifference != value)
                    _DB2expectedFieldDifference = value;
            }
        }
        
        public int DB2ExpectedDifferences
        {
            get { return _DB2expectedDifferences; }
            set
            {
                if (_DB2expectedDifferences != value)
                    _DB2expectedDifferences = value;
            }
        }

        public int DB2ExpectedSimilarities
        {
            get { return _DB2expectedSimilarities; }
            set
            {
                if (_DB2expectedSimilarities != value)
                    _DB2expectedSimilarities = value;
            }
        }

        #endregion // Database Two Properties

        #region Properties

        public int NumberOfCompositeFields
        {
            get { return _numCompositeFields; }
            set
            {
                if (_numCompositeFields != value)
                    _numCompositeFields = value;
            }
        }

        public int NumberOfCompositeChild
        {
            get { return _numCompositeChild; }
            set
            {
                if (_numCompositeChild != value)
                    _numCompositeChild = value;
            }
        }

        #endregion // Properties

        #region Methods

        // Create a list database storage which contains similar nodes - can be an arbitrary number of nodes in EACH database. Both databases doesn't need to have equal number of nodes.
        // Create a list database storage which contains different nodes - can be an arbitrary number of nodes in EACH database. Both databases doesn't need to have equal number of nodes.

        /// <summary>
        /// Creates a single database with the specified number of nodes. The database index can either be 1 or 2.
        /// It returns nothing if an invalid database index is passed.
        /// </summary>
        /// <param name="databaseIndex"></param>
        /// <param name="totalNodes"></param>
        /// <returns></returns>
        public IDatabase CreateDatabase(int databaseIndex, int totalNodes)
        {
            if (databaseIndex < 1 || databaseIndex > 2)
                return null;

            ObservableCollection<IDataNode> nodes = new ObservableCollection<IDataNode>();
            int numberOfDifferentNodes = 0;
            for(int i = 0; i < totalNodes; i++)
            {
                nodes.Add(CreateDataNode(databaseIndex, string.Format("Node{0}", i), numberOfDifferentNodes < (databaseIndex==1 ? _DB1expectedDifferences : _DB2expectedDifferences)));
                numberOfDifferentNodes++;
            }
            return _factory.CreateDatabase(databaseIndex == 1 ? "Database 1" : "Database 2", nodes);
        }

        /// <summary>
        /// Creates a data node which has the given name. The boolean 
        /// parameter indicates whether the fields should be different or not.
        /// </summary>
        /// <param name="databaseIndex"></param>
        /// <param name="nodeName"></param>
        /// <param name="different"></param>
        /// <returns></returns>
        private IDataNode CreateDataNode(int databaseIndex, string nodeName, bool different)
        {
            ObservableCollection<IField> fields = new ObservableCollection<IField>();
            int numDifferent = 0;
            // Create Raw Fields
            for (int i = 0; i < (databaseIndex == 1 ? _DB1NumberOfFields : _DB2NumberOfFields); i++)
            {
                fields.Add(CreateRawField(databaseIndex, string.Format("RawField{0}", i), numDifferent < (databaseIndex == 1 ? _DB1expectedFieldDifference : _DB2expectedFieldDifference)));
                numDifferent++;
            }
            // Create Composite Fields
            for(int i = 0; i < _numCompositeFields; i++)
            {
                fields.Add(CreateCompositeField(databaseIndex, string.Format("Composite{0}", i), numDifferent < (databaseIndex == 1 ? _DB1expectedDifferences : _DB2expectedDifferences));
                numDifferent++;
            }
            return _factory.CreateNode(nodeName, fields);
        }

        /// <summary>
        /// Creates a raw field.
        /// </summary>
        /// <param name="databaseIndex"></param>
        /// <param name="fieldname"></param>
        /// <param name="different"></param>
        /// <returns></returns>
        private IField CreateRawField(int databaseIndex, string fieldname, bool different)
        {
            return _factory.CreateRawField(fieldname, databaseIndex == 1 && different ? "Value 1" : "Value2");
        }

        /// <summary>
        /// Creates a composite field.
        /// </summary>
        /// <param name="databaseIndex"></param>
        /// <param name="fieldName"></param>
        /// <param name="different"></param>
        /// <returns></returns>
        private IField CreateCompositeField(int databaseIndex, string fieldName, bool different)
        {
            CompositeField root = (CompositeField)_factory.CreateCompositeField(fieldName);
            IField[] fields = new IField[5];
            // Create raw fields
            for(int i = 0; i < fields.Length; i++)
            {
                fields[i] = CreateRawField(databaseIndex, String.Format("RawField{0}", i), different);
            }
            if (_numCompositeChild == 0)
            {
                fields.ToList().ForEach(field => root.Fields.Add(field));
            }
            else
            {
                for(int i = 0; i < _numCompositeChild; i++)
                {
                    root.Fields.Add(_factory.CreateCompositeField(String.Format("Child{0}", i), fields));
                }
            }
            return root;
        }

        #endregion // Methods
    }
}
