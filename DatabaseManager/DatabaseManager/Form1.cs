﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;

namespace DatabaseManager
{
    public partial class Form1 : Form
    {
        //For moving Window with no border style / title bar.
        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        //Fields
        Networking network = new Networking();
        Table t;
        ReSize resize = new ReSize();   // ReSize Class "/\" To Help Resize Form <None Style>
        TableSchema ts;
        List<Columns> cols;
        List<Tables> tl;
       

        //For ReSize
        private const int cGrip = 16;      // Grip size
        private const int cCaption = 32;   // Caption bar height;

        public Form1()
        {
            InitializeComponent();
            this.SetStyle(ControlStyles.ResizeRedraw, true);
            ts = new TableSchema(network.GetConnectionString());
            cols = ts.ColumnList;
            tl = ts.TableList;
            DisplayTree();
        }
     

        private void DisplayTree()
        {
           // tl.Sort();

            foreach (Tables table in tl)
            {

                if(!treeView1.Nodes.ContainsKey(table.SchemaName))
                {
                    treeView1.Nodes.Add(table.SchemaName, table.SchemaName);
                    
                }           

            }
            foreach (Tables table in tl)
            {
                if(!treeView1.Nodes[table.SchemaName].Nodes.ContainsKey(table.TableName))
                {
                    treeView1.Nodes[table.SchemaName].Nodes.Add(table.TableName);
                }
               
            }
        }

        // grabs the table from the table class and displays it in datagridview1
        private void DisplayTable1(Task task)
        {
            t = network.GetTable();
            List<Column> columns = t.GetColumns();
            List<Row> rows = t.GetRows();

            foreach (Column col in columns)
            {
                dataGridView1.Invoke(new Action(() => { dataGridView1.Columns.Add(col.Name, col.Name); }));
            }

            int count = 0;

            foreach (Row row in rows)
            {

                object[] o = row.GetRowData();

                dataGridView1.Invoke(new Action(() => { dataGridView1.Rows.Insert(count, o); }));
                    count++;                 
 
            }
        }            
        
        // sends the query typed in queryTextBox to the Query method of the networking class
        private  void queryButton_Click(object sender, EventArgs e)
        {
            try
            {
                clearTable();
                object query = queryTextBox.Text;
                Action <object> action;
                action = network.Query;
                Task task = new Task(action, query);
                Task task2 = task.ContinueWith(DisplayTable1);
                task.Start();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.InnerException.ToString());
                MessageBox.Show(ex.HelpLink);
                MessageBox.Show(ex.HResult.ToString());
                MessageBox.Show(ex.Source.ToString());
            }
        }

        private void doQuery(object query, object schema)
        {
            try
            {
                clearTable();
                //Action<object, object> action;
                //action = network.Query1;
                Task task = new Task(() => network.Query1(query, schema));
                //Task task = new Task(action, query);
                Task task2 = task.ContinueWith(DisplayTable1);
                task.Start();
            }
           catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.InnerException.ToString());
                MessageBox.Show(ex.HelpLink);
                MessageBox.Show(ex.HResult.ToString());
                MessageBox.Show(ex.Source.ToString());
            }
        }
        // clears datagridview1 rows & columns, as well as empties the row and column lists in the table class
        private void clearTable()
        {
            try
            {
                if (dataGridView1.Columns.Count > 0)
                {
                    int count = dataGridView1.Columns.Count;
                    for (int index = 0; index < count; index++)
                    {
                        dataGridView1.Columns.RemoveAt(0);
                    }

                    t.ClearTable();

                    dataGridView1.Rows.Clear();


                }
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show(ex.StackTrace);
                MessageBox.Show(ex.InnerException.ToString());
                MessageBox.Show(ex.HelpLink);
                MessageBox.Show(ex.HResult.ToString());
                MessageBox.Show(ex.Source.ToString());
            }
            
        }

        // clear button
        private void clearButton_Click(object sender, EventArgs e)
        {         
                clearTable();          
        }

        // exit button
        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        // maximize button
        private void maximizeButton_Click(object sender, EventArgs e)
        {
            maxmimizeScreen();
        }

        // if the screen is maximized it sets it to normal state, otherwise it maximizes it
        private void maxmimizeScreen()
        {
            if (WindowState == FormWindowState.Maximized)
            {
                WindowState = FormWindowState.Normal;
            }
            else
            {
                WindowState = FormWindowState.Maximized;
            }
        }

        // event for when the panel gets dragged. allows window to be moved with FormBorderStyle set to none.
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        // minimize button
        private void minimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

        // update button
        private void updateButton_Click(object sender, EventArgs e)
        {
            clearTable();
            object query = queryTextBox.Text;
            Action<object> action;
            action = network.Update;
            Task task = new Task(action, query);
            task.Start();
        }

        // methods for ReSize class to resize the form with FormBorderStyle.None
        //----------------------------------------------------------------------

        public override Size MinimumSize // forms minimum size
        {
            get
            {
                return base.MinimumSize;
            }
            set
            {
                base.MinimumSize = new Size(179, 51); 
            }
        }

        //
        //override  WndProc  
        //
        protected override void WndProc(ref Message m) // sends resize messages to 
        {
            //****************************************************************************

            int x = (int)(m.LParam.ToInt64() & 0xFFFF);               //get x mouse position
            int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);   //get y mouse position  you can gave (x,y) it from "MouseEventArgs" too
            Point pt = PointToClient(new Point(x, y));

            if (m.Msg == 0x84)
            {
                switch (resize.getMosuePosition(pt, this))
                {
                    case "l": m.Result = (IntPtr)10; return;  // the Mouse on Left Form
                    case "r": m.Result = (IntPtr)11; return;  // the Mouse on Right Form
                    case "a": m.Result = (IntPtr)12; return;
                    case "la": m.Result = (IntPtr)13; return;
                    case "ra": m.Result = (IntPtr)14; return;
                    case "u": m.Result = (IntPtr)15; return;
                    case "lu": m.Result = (IntPtr)16; return;
                    case "ru": m.Result = (IntPtr)17; return; // the Mouse on Right_Under Form
                    case "": m.Result = pt.Y < 32 /*mouse on title Bar*/ ? (IntPtr)2 : (IntPtr)1; return;

                }
            }

            base.WndProc(ref m);

        }

        // datagridview1 cell value changed event. Creates an object to store the index of cell, and data.
        // stores the data in the Table class.
        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            string primaryKey = null;
            string colType = null;

            List<PrimaryKey> primaryKeys = ts.PrimaryKeyList;
            try
            {
                object objId = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                object objName = dataGridView1.Columns[e.ColumnIndex].Name;
                object value = dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;

                int id = Convert.ToInt32(objId);
                string name = objName.ToString();

                string schema = treeView1.SelectedNode.Parent.Name;
                string tableName = treeView1.SelectedNode.Text;
               
                foreach(PrimaryKey pk in primaryKeys)
                {
                    if(pk.TableName == tableName)
                    {
                        primaryKey = pk.FieldName;
                    }
                }    
                
                foreach(Columns c in cols)
                {
                    if(c.TableName == tableName && c.FieldName == name)
                    {
                        colType = c.ColumnType;
                    }
                }         

                CellChange change = new CellChange(id, name, value, schema, tableName, primaryKey, colType);
                t.AddChange(change);

                //MessageBox.Show("colindex" + e.ColumnIndex.ToString());
                //MessageBox.Show("rowindex" + e.RowIndex.ToString());
                //MessageBox.Show("Column Name: " + dataGridView1.Columns[e.ColumnIndex].Name);             
                //MessageBox.Show("New Value:  " + dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
                //MessageBox.Show("Primary Key ID# " + dataGridView1.Rows[e.RowIndex].Cells[0].Value);
               
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        // grabs the changes list from Table class and sends them to the Networking classes Save method.
        // this in turn performs the SQL Update command for each change to update the DB.
        private void saveButton_Click(object sender, EventArgs e)
        {
            List<CellChange> changes = t.GetChanges();

            foreach(CellChange change in changes)
            {

                //TODO: Add those to be dynamically determined for the command string below.
                //TODO: Implement a way to determine tha value type of change.Value
                //TODO: So that this method can deal with all value types supported by SQL.
                //TODO:  Look at implementing a dictionary as shown in link below
                // https://stackoverflow.com/questions/1720707/getschemacolumns-return-datatype
                //TODO: Look into foriegn key restrictions and what it means

                Console.WriteLine(change.ColType);
                if(change.ColType == "nvarchar")
                {
                    string query = $"UPDATE {change.Schema}.{change.Table} SET {change.ColName} = '{change.Value}' WHERE {change.PrimaryKeyName} = {change.Id};";
                    network.Save(query);
                }
                
                if(change.ColType == "int")
                {
                    string query = $"UPDATE {change.Schema}.{change.Table} SET {change.ColName} = {change.Value} WHERE {change.PrimaryKeyName} = {change.Id};";
                    network.Save(query);
                }

                if (change.ColType == "tinyint")
                {
                    string query = $"UPDATE {change.Schema}.{change.Table} SET {change.ColName} = {change.Value} WHERE {change.PrimaryKeyName} = {change.Id};";
                    network.Save(query);
                }
            }
           
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            treeView1.SelectedNode = e.Node; // makes sure that the node gets selected if user clicks NEAR the node (consider changing to isSelected event)
            

            if(e.Node.Parent != null) // if the node IS  a parent node then it should have a NULL parent
            {
                doQuery(e.Node.Text, e.Node.Parent.Text); // only fires if the node is a child node (table instead of schema)
            }         
            
        }   

        private void queryTextBox_Enter(object sender, EventArgs e)
        {
            if (queryTextBox.Text == "Enter your SQL query here...")
            {
                queryTextBox.Text = String.Empty;
            }
        }

        private void queryTextBox_Leave(object sender, EventArgs e)
        {
            if(queryTextBox.Text == String.Empty)
            {
                queryTextBox.Text = "Enter your SQL query here...";
            }
        }
    }
}

