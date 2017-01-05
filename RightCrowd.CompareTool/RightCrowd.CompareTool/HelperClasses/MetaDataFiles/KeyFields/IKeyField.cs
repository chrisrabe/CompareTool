using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses.MetaDataFiles.KeyFields
{
    public interface IKeyField
    {
        string Name { get; set; }
        ICollection<string> Keys { get; set; }
    }
}
