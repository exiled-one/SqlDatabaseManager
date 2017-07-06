using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    class Column
    {
        private string name = null;

        private string dataType = String.Empty;

        public Column(string n,  string d)
        {
            this.name = n;           
            this.dataType = d;

        }

        // read only properties

        public string Name
        {
            get { return this.name; }
        }

        public string DataT
        {
            get { return this.dataType; }
        }
    }
}
