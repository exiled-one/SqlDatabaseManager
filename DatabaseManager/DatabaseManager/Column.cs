using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{

    // a column is only the column header because all of the data is stored in the rows
    class Column
    {
        private string name = null;

        private string dataType = String.Empty;

        public Column(string n,  string d) // a column only has a column name & a data type
        {
            this.name = n;           
            this.dataType = d;

        }

        // read only properties

        public string Name  // returns name
        {
            get { return this.name; }
        }

        public string DataT  // returns data type
        {
            get { return this.dataType; }
        }
    }
}
