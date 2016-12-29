using RightCrowd.CompareTool.HelperClasses.MetaData.KeyField;
using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses.MetaData
{
    public interface IMetaData
    {
        ICollection<IKeyField> KeyFields { get; set; }
    }
}
