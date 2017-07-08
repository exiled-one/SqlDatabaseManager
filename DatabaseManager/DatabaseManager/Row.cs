using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseManager
{   
    class Row // a row consists of a single row and all of the cells that are in it
    {
        private object[] rowData;
        private static int numberOfRows = 0;

        // row constructor accepts a params array of objects
        //TODO: check if the boxing/ unboxing is costing a lot of performance

        public Row(params object[] obj)
        {
            rowData = new object[obj.Length];

            for(int count = 0; count < obj.Length; count++)
            {
                rowData[count] = obj[count];
            }

            numberOfRows++;
        }

      
        // returns the array that holds all of the objects in a single row
        public object[] GetRowData()
        {
            return rowData;
        }
    }
}
