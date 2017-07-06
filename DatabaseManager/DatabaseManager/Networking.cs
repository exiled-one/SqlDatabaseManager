using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Data.SqlClient;

namespace DatabaseManager
{
    class Networking
    {
        Table t = new Table("EmailAddress", 5, 0);
        string myConnectionString;

        public Networking()
        {
            myConnectionString = @"Server=DESKTOP-55LU8MU\TEST;Database=TESTDB;User Id=admin; Password=1234qwert;";
        }

        public Table GetTable()
        {
            return t;
        }

        public void Save(string query)
        {
            SqlConnection conn = new SqlConnection(myConnectionString);

            conn.Open();

            Console.WriteLine("Connected for saving...");

            SqlCommand cmd = new SqlCommand(query, conn);

            int result = cmd.ExecuteNonQuery();

            Console.WriteLine($"Result: {result}");

            conn.Close();

        }
        public async void Update(object objQuery)
        {

            string query = objQuery as string;

            SqlConnection conn = new SqlConnection(myConnectionString);

            conn.Open();

            Console.WriteLine("Connected for updating...");

            SqlCommand cmd = new SqlCommand(query, conn);

            int result = cmd.ExecuteNonQuery();

            Console.WriteLine($"Result: {result}");

            conn.Close();
            

        }
        public async void Query(object objQuery)
        {
            

            try
            {

                string query = objQuery as string;                             

                SqlConnection conn = new SqlConnection(myConnectionString);

                conn.Open();

                Console.WriteLine("Connected...");               

                SqlCommand cmd = new SqlCommand(query, conn);

                SqlDataReader rdr = cmd.ExecuteReader();               

                object[] arrayRow = new object[rdr.FieldCount];

                while (rdr.Read())
                {
                    for (int count = 0; count < rdr.FieldCount; count++)
                    {
                        arrayRow[count] = rdr[count];
                    }
                    Row row = new DatabaseManager.Row(arrayRow);
                    t.AddRow(row);
                }

                for(int index = 0; index < rdr.FieldCount; index++)
                {
                    Column column = new Column(rdr.GetName(index), rdr.GetDataTypeName(index));
                    t.AddColumn(column);
                }

                rdr.Close();
               


            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
               
            }
        }




    }


    }
