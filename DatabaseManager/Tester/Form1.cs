using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Tester
{
    public partial class Form1 : Form
    {
        //TODO: Refactor project to use TableSchema instead
        //TODO: implement the treeview1 shown below in databaseManager project.

        string myConnectionString = @"Server=DESKTOP-55LU8MU\TEST;Database=TESTDB;User Id=admin; Password=1234qwert;";
        TableSchema ts;
        List<Columns> cols;
        List<string> tl;


        public Form1()
        {
            InitializeComponent();
            ts = new TableSchema(myConnectionString);
            cols = ts.ColumnList;
            tl = ts.TableList;
        }



        private void button1_Click(object sender, EventArgs e)
        {
            DisplayTree();
        }

        private void DisplayTree()
        {
            tl.Sort();

            foreach(string s in tl)
            {
                treeView1.Nodes.Add(s, s);

                foreach (Columns col in cols)
                {
                    
                    if(s == col.TableName)
                    {
                        treeView1.Nodes[s].Nodes.Add(col.FieldName);
                    }                    
                    
                }
            }
            
           
        }
    }
}
