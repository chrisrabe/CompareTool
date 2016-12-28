using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RightCrowd.CompareTool.HelperClasses
{
    public class MetaData
    {
        public MetaData()
        {
            KeyFields = new List<DatabaseKeyField>
            {
                new DatabaseKeyField("AccessLevelPermissions", "AccessLevel")
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
