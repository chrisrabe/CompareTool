using System.Collections.Generic;
using RightCrowd.CompareTool.HelperClasses.MetaDataFiles.KeyFields;

namespace RightCrowd.CompareTool.HelperClasses.MetaDataFiles
{
    /// <summary>
    /// This class determines the element -> key pairs. It is used to alter how the
    /// files are parsed.
    /// </summary>
    public class MetaData : IMetaData
    {
        private ICollection<IKeyField> _keyFields;

        public MetaData()
        {
            KeyFields = new List<IKeyField>
            {
                new KeyField("AccessLevelPermissions", "AccessLevel")
            };
        }

        /// <summary>
        /// Gets or sets the key fields of this metadata.
        /// </summary>
        public ICollection<IKeyField> KeyFields
        {
            get
            {
                if (_keyFields == null)
                    _keyFields = new List<IKeyField>();
                return _keyFields;
            }
            set { _keyFields = value; }
        }
    }

    public class KeyField : IKeyField
    {
        public KeyField(string name, string key)
        {
            Name = name;
            Key = key;
        }

        public string Name { get; set; }

        public string Key { get; set; }
    }
}
