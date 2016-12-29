using RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataComparators;
using RightCrowd.CompareTool.HelperClasses.MetaData.KeyField;
using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses.MetaData
{
    public class NodeMetaData : IMetaData
    {
        public NodeMetaData()
        {
            KeyFields = new List<IKeyField>
            {
                new DatabaseKeyField("AccessLevelPermissions", "AccessLevel", new MultipleDataComparator()) // Need to read an XML file
            };
        }

        public ICollection<IKeyField> KeyFields { get; set; }
    }

    public class DatabaseKeyField : IKeyField
    {
        public DatabaseKeyField(IKeyField key) : this(key.DatabaseName, key.KeyElementName, new MultipleDataComparator()) { }

        public DatabaseKeyField(string databaseName, string keyElementName, IDataComparator dataComparator)
        {
            DatabaseName = databaseName;
            KeyElementName = keyElementName;
            DataComparator = dataComparator;
        }

        public string DatabaseName { get; set; }
        public string KeyElementName { get; set; }
        public IDataComparator DataComparator { get; set; }
    }
}
