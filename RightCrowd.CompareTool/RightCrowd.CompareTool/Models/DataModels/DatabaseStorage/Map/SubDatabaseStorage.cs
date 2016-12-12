using System.Collections.Generic;
using RightCrowd.CompareTool.Models.DataModels.Database;
using System.Linq;

namespace RightCrowd.CompareTool.Models.DataModels.DatabaseStorage.Map
{
    /// <summary>
    /// Stores databases containing a specific type of data node.
    /// Each databases is assigned a key string relating to the type
    /// of data nodes stored inside it.
    /// </summary>
    public class SubDatabaseStorage : IMapDatabaseStorage
    {
        #region Fields

        private Dictionary<string, IDatabase> _subDatabases;

        #endregion // Fields

        #region Constructors

        public SubDatabaseStorage()
        {
            _subDatabases = new Dictionary<string, IDatabase>();
        }

        #endregion // Constructors

        #region IMapDatabaseStorage Members

        /// <summary>
        /// Gets or sets the database with the given key. 
        /// If the key doesn't exist, then it returns null.
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public IDatabase this[string key]
        {
            get
            {
                IDatabase tmp;
                if (!_subDatabases.TryGetValue(key, out tmp))
                    return null; // doesn't exist, so return nothing
                else
                    return tmp;
            }

            set
            {
                if (_subDatabases.ContainsKey(key))
                    _subDatabases[key] = value;
                else
                    _subDatabases.Add(key, value);
            }
        }

        /// <summary>
        /// Returns the sub databases inside this storage.
        /// </summary>
        public IDatabase[] Databases
        {
            get
            {
                return _subDatabases.Values.ToArray();
            }
        }

        #endregion // IMapDatabaseStorage Members
    }
}
