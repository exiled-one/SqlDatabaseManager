using System;
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

        public Form1()
        {
            InitializeComponent();
        }

        public const int WM_NCLBUTTONDOWN = 0xA1;
        public const int HTCAPTION = 0x2;
        [DllImport("User32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        Networking network = new Networking();
        Table t;


        private void DisplayTable1(Task task)
        {
            t  = network.GetTable();
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

        private void clearButton_Click(object sender, EventArgs e)
        {         
                clearTable();          
        }

        private void exitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void maximizeButton_Click(object sender, EventArgs e)
        {
            maxmimizeScreen();
        }

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

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HTCAPTION, 0);
            }
        }

        private void minimizeButton_Click(object sender, EventArgs e)
        {
            WindowState = FormWindowState.Minimized;
        }

       
    }
}
