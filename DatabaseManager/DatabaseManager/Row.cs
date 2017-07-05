using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseManager
{
    class Row
    {
        private object[] rowData;
        private static int numberOfRows = 0;

        public Row(params object[] obj)
        {
            rowData = new object[obj.Length];

            for(int count = 0; count < obj.Length; count++)
            {
                rowData[count] = obj[count];
            }

            numberOfRows++;
        }

      

        public object[] GetRowData()
        {
            return rowData;
        }
    }
}
