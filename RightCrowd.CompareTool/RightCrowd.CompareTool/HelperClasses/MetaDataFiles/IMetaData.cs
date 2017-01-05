using RightCrowd.CompareTool.HelperClasses.MetaDataFiles.KeyFields;
using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses.MetaDataFiles
{
    public interface IMetaData
    {
        ICollection<IKeyField> KeyFields { get; set; }
    }
}
