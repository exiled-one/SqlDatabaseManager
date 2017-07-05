using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseManager
{
    class Table
    {
        // The table class represents the top level entity or the table itself //

        private string name = null;
        private int numberOfColumns = 0;
        private int indexOfPrimaryKey = 0;

        private List<Column> columns;
        private List<Row> rows;

        public Table(string n, int cols, int key)
        {
            this.name = n;
            this.numberOfColumns = cols;
            this.indexOfPrimaryKey = key;
            columns = new List<Column>();
            rows = new List<Row>();

            //Column c = new Column("BusinessEntityID", DataType.Integer);
            //Column c1 = new Column("EmailAddressID", DataType.Integer);
            //Column c2 = new Column("EmailAddress", DataType.String);
            //Column c3 = new Column("GUID", DataType.String);
            //Column c4 = new Column("Date", DataType.String);

            //AddColumn(c);
            //AddColumn(c1);
            //AddColumn(c2);
            //AddColumn(c3);
            //AddColumn(c4);

        }

        //properties

        public string Name
        {
            get { return this.name; }
        }

        public int NumberOfColumns
        {
            get { return this.numberOfColumns; }
        }

        public int IndexOfPrimaryKey
        {
            get { return this.indexOfPrimaryKey; }
        }

       //methods

        public void AddColumn(Column c)
        {   
            columns.Add(c);
        }

        public void AddRow(Row r)
        {
            rows.Add(r);
        }

        public List<Column> GetColumns()
        {
            return columns;
        }

        public List<Row> GetRows()
        {
            return rows;
        }

        public void ClearTable()
        {
            rows.Clear();
            columns.Clear();
        }
    }
}
