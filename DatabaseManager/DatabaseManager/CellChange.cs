using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    class CellChange
    {
        private string colName;
        private object value1;
        private int id;

        public CellChange(int i, string c, object v)
        {
            id = i;
            colName = c;
            value1 = v;
        }

        

        public int Id
        {
            get { return id; }
           
        }

        public object Value
        {
            get { return value1; }
           
        }

        public string ColName
        {
            get { return colName; }
           
        }


    }
}
