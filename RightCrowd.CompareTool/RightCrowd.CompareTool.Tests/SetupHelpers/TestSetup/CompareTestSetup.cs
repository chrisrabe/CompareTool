using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using RightCrowd.CompareTool.Tests.SetupHelpers.Factories;
using System.Collections.ObjectModel;
using System;
using System.Linq;
using RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.List;

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
        private int _DB1expectedFieldDifference;
        private int _DB1NumberOfRawFields;

        private int _DB2expectedDifferences;
        private int _DB2expectedFieldDifference;
        private int _DB2NumberOfRawFields;

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

        /// <summary>
        /// Gets or sets the number of fields in each node.
        /// </summary>
        public int DB1NumberOfRawFields
        {
            get { return _DB1NumberOfRawFields; }
            set
            {
                if (_DB1NumberOfRawFields != value)
                    _DB1NumberOfRawFields = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of difference in fields
        /// in each node.
        /// </summary>
        public int DB1ExpectedFieldDifference
        {
            get { return _DB1expectedFieldDifference; }
            set
            {
                if (_DB1expectedFieldDifference != value)
                    _DB1expectedFieldDifference = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of difference in nodes
        /// in each database. This should consider the
        /// number of missing nodes.
        /// </summary>
        public int DB1ExpectedDifferences
        {
            get { return _DB1expectedDifferences; }
            set
            {
                if (_DB1expectedDifferences != value)
                    _DB1expectedDifferences = value;
            }
        }

        #endregion // Database One Properties

        #region Database Two Properties

        /// <summary>
        /// Gets or sets the number of fields in
        /// each node in database two.
        /// </summary>
        public int DB2NumberOfRawFields
        {
            get { return _DB2NumberOfRawFields; }
            set
            {
                if (_DB2NumberOfRawFields != value)
                    _DB2NumberOfRawFields = value;
            }
        }

        /// <summary>
        /// Gets or sets the number of difference
        /// in fields 
        /// </summary>
        public int DB2ExpectedFieldDifference
        {
            get { return _DB2expectedFieldDifference; }
            set
            {
                if (_DB2expectedFieldDifference != value)
                    _DB2expectedFieldDifference = value;
            }
        }

        /// <summary>
        /// Gets or sets the expected difference
        /// in nodes inside a database.
        /// </summary>
        public int DB2ExpectedDifferences
        {
            get { return _DB2expectedDifferences; }
            set
            {
                if (_DB2expectedDifferences != value)
                    _DB2expectedDifferences = value;
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

        public IListDatabaseStorage CreateSimilarDatabases(int totalNodesInDB1, int totalNodesInDB2)
        {
            // Set up validation checks
            if (!validTotalNodes(totalNodesInDB1, totalNodesInDB2))
                throw new Exception("Expected difference is less than total number of nodes. Invalid set up.");
            if (DB1ExpectedDifferences != 0 || DB2ExpectedDifferences != 0)
                throw new Exception("Difference should be set to zero when creating similar database");
            if (DB1ExpectedFieldDifference != 0 || DB2ExpectedFieldDifference != 0)
                throw new Exception("Difference in fields should be set to zero when creating similar database");
            if (totalNodesInDB1 != totalNodesInDB2)
                throw new Exception("Both nodes must be equal in size to be similar");
            if (DB1NumberOfRawFields != DB2NumberOfRawFields)
                throw new Exception("Number of raw fields must be equal to be similar");

            IListDatabaseStorage storage = new TwoDatabaseStorage();
            storage[0] = CreateDatabase(1, totalNodesInDB1);
            storage[1] = CreateDatabase(2, totalNodesInDB2);
            return storage;
        }

        public IListDatabaseStorage CreateDifferentDatabases(int totalNodesInDB1, int totalNodesInDB2)
        {
            // Set up validation checks
            if (!validTotalNodes(totalNodesInDB1, totalNodesInDB2))
                throw new Exception("Expected difference is less than total number of nodes. Invalid set up.");
            if (DB1ExpectedFieldDifference > (DB1NumberOfRawFields + NumberOfCompositeFields))
                throw new Exception("Expected field difference is bigger than total fields inside each node in DB1.");
            if (DB2ExpectedFieldDifference > (DB2NumberOfRawFields + NumberOfCompositeFields))
                throw new Exception("Expected field difference is bigger than total fields inside each node in DB2.");

            IListDatabaseStorage storage = new TwoDatabaseStorage();
            storage[0] = CreateDatabase(1, totalNodesInDB1);
            storage[1] = CreateDatabase(2, totalNodesInDB2);
            return storage;
        }

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
            for (int i = 0; i < totalNodes; i++)
            {
                nodes.Add(CreateDataNode(databaseIndex, string.Format("Node{0}", i), numberOfDifferentNodes < (databaseIndex == 1 ? _DB1expectedDifferences : _DB2expectedDifferences)));
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
            for (int i = 0; i < (databaseIndex == 1 ? _DB1NumberOfRawFields : _DB2NumberOfRawFields); i++)
            {
                if (different)
                {
                    fields.Add(CreateRawField(databaseIndex, string.Format("RawField{0}", i), numDifferent < (databaseIndex == 1 ? _DB1expectedFieldDifference : _DB2expectedFieldDifference)));
                    numDifferent++;
                }else
                {
                    fields.Add(CreateRawField(databaseIndex, string.Format("RawField{0}", i), false));
                }
            }
            // Create Composite Fields
            for (int i = 0; i < _numCompositeFields; i++)
            {
                if (different)
                {
                    fields.Add(CreateCompositeField(databaseIndex, string.Format("Composite{0}", i), numDifferent < (databaseIndex == 1 ? _DB1expectedFieldDifference : _DB2expectedFieldDifference)));
                    numDifferent++;
                }else
                {
                    fields.Add(CreateCompositeField(databaseIndex, string.Format("Composite{0}", i), false));
                }
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
            for (int i = 0; i < fields.Length; i++)
            {
                fields[i] = CreateRawField(databaseIndex, String.Format("RawField{0}", i), different);
            }
            if (_numCompositeChild == 0)
            {
                fields.ToList().ForEach(field => root.Fields.Add(field));
            }
            else
            {
                for (int i = 0; i < _numCompositeChild; i++)
                {
                    root.Fields.Add(_factory.CreateCompositeField(String.Format("Child{0}", i), fields));
                }
            }
            return root;
        }

        #endregion // Methods

        #region Helper Methods

        /// <summary>
        /// This ensures that the total number of differences given in the set up
        /// is less than the total number of nodes.
        /// </summary>
        /// <param name="totalNodesInDB1"></param>
        /// <param name="totalNodesInDB2"></param>
        /// <returns></returns>
        private bool validTotalNodes(int totalNodesInDB1, int totalNodesInDB2)
        {
            if (DB1ExpectedDifferences > totalNodesInDB1)
                return false;
            if (DB2ExpectedDifferences > totalNodesInDB2)
                return false;
            return true;
        }

        #endregion // Helper Methods
    }
}
