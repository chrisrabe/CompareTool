using RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataComparators;
using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses
{
    public class MetaData
    {
        public MetaData()
        {
            KeyFields = new List<DatabaseKeyField>
            {
                new DatabaseKeyField("AccessLevelPermissions", "AccessLevel", new MultipleDataComparator()) // Need to read an XML file
            };
        }  

        public ICollection<DatabaseKeyField> KeyFields { get; set; }
    }

    public class DatabaseKeyField
    {
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
