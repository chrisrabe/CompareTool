using System.Collections.ObjectModel;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.DataNode;
using RightCrowd.CompareTool.Tests.SetupHelpers.Factories;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using System.Linq;

namespace RightCrowd.CompareTool.Tests.SetupHelpers.TestSetup
{
    /// <summary>
    /// This class is responsible for creating mock databases for the compare test.
    /// </summary>
    internal class CompareTestSetup
    {
        private DataModelFactory _factory;

        public CompareTestSetup()
        {
            _factory = new DataModelFactory();
        }

        /// <summary>
        /// Creates a mock database one. It takes in a boolean parameter which
        /// indicates whether or not you would put composite fields into the 
        /// data node. It takes a database of 1 or 2.
        /// </summary>
        /// <param name="putComposite"></param>
        /// <param name="databaseIndex"></param>
        /// <returns></returns>
        public IDatabase CreateMockDatabase(int databaseIndex, bool putComposite, bool nestedComposite)
        {
            if (databaseIndex < 1 || databaseIndex > 2)
                return null; // exit out the method if invalid database index
            ObservableCollection<IDataNode> nodes = createMockDataNode(databaseIndex, putComposite, nestedComposite);
            return _factory.CreateDatabase(databaseIndex == 1 ? "Mock DB1" : "Mock DB2", nodes);
        }

        private ObservableCollection<IDataNode> createMockDataNode(int databaseIndex, bool putComposite, bool nestedComposite)
        {
            ObservableCollection<IField> fields = createFields(databaseIndex, putComposite, nestedComposite, CreateRawFields(databaseIndex));
            ObservableCollection<IDataNode> tmp = new ObservableCollection<IDataNode>();
            tmp.Add(_factory.CreateNode("MockNode", fields));
            return tmp;
        }

        /// <summary>
        /// Creates fields. Raw fields are first added into the collection and
        /// if the putComposite parameter is true, it inserts a composite field.
        /// </summary>
        /// <param name="databaseIndex"></param>
        /// <param name="putComposite"></param>
        /// <param name="rawFields"></param>
        /// <returns></returns>
        private ObservableCollection<IField> createFields(int databaseIndex, bool putComposite, bool nestedComposite, params RawField[] rawFields)
        {
            ObservableCollection<IField> tmp = new ObservableCollection<IField>();
            rawFields.ToList().ForEach(rawField => tmp.Add(rawField));
            if (putComposite)
                tmp.Add(CreateCompositeFields(databaseIndex, nestedComposite));
            return tmp;
        }

        /// <summary>
        /// Creates raw fields depending on the database index. The database index
        /// must be either 1 or 2. This method creates an array containing six elements.
        /// It creates four fields which are independent (which means that they will be
        /// similar regardless of the database index) and two fields which is different.
        /// </summary>
        /// <param name="databaseIndex"></param>
        /// <returns></returns>
        private RawField[] CreateRawFields(int databaseIndex)
        {
            RawField[] fields = new RawField[6];
            fields[0] = (RawField)_factory.CreateRawField("RawField1", databaseIndex == 1 ? "Value1" : "Value2");
            fields[1] = (RawField)_factory.CreateRawField("RawField2", databaseIndex == 1 ? "SomeName1" : "SomeName2");
            for (int i = 2; i < fields.Length; i++)
            {
                fields[i] = (RawField)_factory.CreateRawField(string.Format("RawField{0}", (i + 1)), "FieldValue");
            }
            return fields;
        }

        /// <summary>
        /// Creates a composite field depending on the database index.
        /// The database index must be either 1 or 2. 
        /// If the nestedComposite parameter is set to true, it creates
        /// a root composite field with two child nodes.
        /// </summary>
        /// <param name="databaseIndex"></param>
        /// <returns></returns>
        private IField CreateCompositeFields(int databaseIndex, bool nestedComposite)
        {
            CompositeField root = new CompositeField(nestedComposite ? "CompositeFields" : "CompositeField");
            if (!nestedComposite)
                CreateRawFields(databaseIndex).ToList().ForEach(rawField => root.Fields.Add(rawField));
            else
            {
                for(int i = 0; i < 2; i++)
                {
                    IField child = _factory.CreateCompositeChild("Composite", CreateRawFields(databaseIndex));
                    root.Fields.Add(child);
                }
            }
            return root;
        }
    }
}
