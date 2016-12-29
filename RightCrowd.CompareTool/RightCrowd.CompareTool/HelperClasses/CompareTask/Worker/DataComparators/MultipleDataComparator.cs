using System.Linq;
using RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataHandlers;
using RightCrowd.CompareTool.Models.DataModels.Database;
using RightCrowd.CompareTool.Models.DataModels.Fields;
using System.Collections.Generic;

namespace RightCrowd.CompareTool.HelperClasses.CompareTask.Worker.DataComparators
{
    public class MultipleDataComparator : IDataComparator
    {
        public IDataHandler Handler { get; set; }

        public void Compare(int index1, int index2, params IDatabase[] databases)
        {
            var db1 = databases[index1];
            var db2 = databases[index2];

            var doesntExist = db1.Data.Where(data1 => !db2.Data.Any(data2 => data1.FileName.Equals(data2.FileName))).ToList();
            var different = db1.Data.Where(data1 => db2.Data.Any(data2 => {
                if (!data1.FileName.Equals(data2.FileName))
                    return false;

                return Compare(index1, data1.Fields, data2.Fields);
            }));

            //first where the other doesn't exist
            doesntExist.ForEach(x =>
            {
                Handler.RecordAsDifferent(index1, x, true);
            });

            //return all others as the same.
            db1.Data.Where(dataNode => !doesntExist.Any(usedDataNode => usedDataNode == dataNode) && !different.Any(usedDataNode => usedDataNode == dataNode))
                    .ToList()
                    .ForEach(dataNode => Handler.RecordAsSimilar(index1, dataNode));
            
        }

        /// <summary>
        /// Returns true if at least one field is different, returns false if none are different
        /// </summary>
        /// <param name="index"></param>
        /// <param name="fields1"></param>
        /// <param name="fields2"></param>
        /// <returns></returns>
        private bool Compare(int index, IEnumerable<IField> fields1, IEnumerable<IField> fields2)
        {
            return !fields1.All(field1 => fields2.Any(field2 => field2.Equals(field1)));
        }
    }
}
