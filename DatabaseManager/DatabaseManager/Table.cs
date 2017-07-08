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
        // it has lists to hold Row objects, which hold all of the cells in a single row
        // and Column objects that hold a single column header only.
        // the data is stored in the rows.

        private string name = null;
        private int numberOfColumns = 0;
        private int indexOfPrimaryKey = 0;

        private List<Column> columns;
        private List<Row> rows;
        private List<CellChange> changes;

        public Table(string n, int cols, int key)
        {
            this.name = n;
            this.numberOfColumns = cols;
            this.indexOfPrimaryKey = key;
            columns = new List<Column>();
            rows = new List<Row>();
            changes = new List<CellChange>();


        }

        //properties

        public string Name  // returns name of table
        {
            get { return this.name; }
        }

        public int NumberOfColumns // returns number of  columns in the table
        {
            get { return this.numberOfColumns; }
        }

        public int IndexOfPrimaryKey // returns the index of the primary key
        {
            get { return this.indexOfPrimaryKey; }
        }

       //methods

        public void AddColumn(Column c) // adds a column to the column list
        {   
            columns.Add(c);
        }

        public void AddRow(Row r) // add a row to the row list
        {
            rows.Add(r);
        }

        public void AddChange(CellChange change) // add a cell to be changed to the changes list
        {
            changes.Add(change);
        }

        public List<CellChange> GetChanges() // return the changes list
        {
            return changes;
        }

        public List<Column> GetColumns() // return the columns list
        {
            return columns;
        }

        public List<Row> GetRows() // return the rows list
        {
            return rows;
        }

        public void ClearTable() // empty the coluns and rows
        {
            //TODO: add method to empty the cell changes when table gets cleared
            rows.Clear();
            columns.Clear();
        }
    }
}
