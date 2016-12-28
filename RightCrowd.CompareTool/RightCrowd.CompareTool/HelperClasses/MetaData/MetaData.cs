using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses
{
    public class MetaData
    {
        public MetaData()
        {
            KeyFields = new List<DatabaseKeyField>
            {
                new DatabaseKeyField("AccessLevelPermissions", "AccessLevel") // Need to read an XML file
            };
        }  

        public ICollection<DatabaseKeyField> KeyFields { get; set; }
    }

    public class DatabaseKeyField
    {
        public DatabaseKeyField(string databaseName, string keyElementName)
        {
            DatabaseName = databaseName;
            KeyElementName = keyElementName;
        }

        public string DatabaseName { get; set; }
        public string KeyElementName { get; set; }
    }
}
