using RightCrowd.CompareTool.HelperClasses.MetaDatas.KeyFields;
using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses.MetaDatas
{
    public interface IMetaData
    {
        ICollection<IKeyField> KeyFields { get; set; }
    }
}
