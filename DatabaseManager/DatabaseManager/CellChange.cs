using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{

    // cell change stores a cell whos value was changed by the user
    // it stores the new value set by user, the column and primary key index of the cell. 
    // between those two peices of information you can identify the cell & form a UPDATE command in SQL
    class CellChange
    {
        private string colName; // column name being changed, needed to format SQL UPDATE statement
        private object value1; //  new value to be sent to database in UPDATE command
        private int id; // id is the primary key index of the cell that needs to be UPDATED.
        private string schema;
        private string table;
        private string primaryKeyName;
        private string colType;

        public CellChange(int i, string c, object v, string s, string t, string pkn, string ct)
        {
            id = i;
            colName = c;
            value1 = v;
            schema = s;
            table = t;
            primaryKeyName = pkn;
            colType = ct;
        }

        

        public int Id // returns primary key index
        {
            get { return id; }
           
        }

        public object Value // new value input by user
        {
            get { return value1; }
           
        }

        public string ColName // column the cell is located in
        {
            get { return colName; }
           
        }

        public string Schema // returns schema
        {
            get { return schema; }

        }

        public string Table // returns table
        {
            get { return table; }

        }

        public string PrimaryKeyName // returns primary key name
        {
            get { return primaryKeyName; }

        }

        public string ColType // String representation of the SQL type of the column being changed
        {
            get { return colType; }
        }

    }
}
