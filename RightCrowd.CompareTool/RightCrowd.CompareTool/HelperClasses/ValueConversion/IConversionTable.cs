using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses.ValueConversion
{
    /// <summary>
    /// Contains all the conversion fields
    /// </summary>
    public interface IConversionTable
    {
        ICollection<IConversionField> Fields { get; set; }
    }
}
