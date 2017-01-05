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
        private ICollection<string> _keys;

        public KeyField(string name, string key) : this(name)
        {
            Keys.Add(key);
        }

        public KeyField(string name)
        {
            Name = name;
        }

        public string Name { get; set; }

        public ICollection<string> Keys
        {
            get
            {
                if (_keys == null)
                    _keys = new List<string>();
                return _keys;
            }
            set { _keys = value; }
        }
    }
}
