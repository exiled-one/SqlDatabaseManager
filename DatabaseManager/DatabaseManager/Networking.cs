using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace DatabaseManager
{
    class Networking
    {
        //TODO: write method to grab the table name when the  first query is run.
        //TODO: or set it up so that the database schema/ list of tables is pulled automatically when
        //TODO: the database is first connected to, and then the table name changes depending on which one
        //TODO: is selected.
        
        
        Table t = new Table("EmailAddress", 5, 0);
        string myConnectionString;
        TableSchema tableSchema;

        public Networking()
        {
            myConnectionString = @"Server=DESKTOP-55LU8MU\TEST;Database=TESTDB;User Id=admin; Password=1234qwert;";
            tableSchema = new TableSchema(myConnectionString);
            
        }

        // returns table object with all of the table data stored in it
        public Table GetTable()
        {
            return t;
        }

        public string GetConnectionString()
        {
            return myConnectionString;
        }
        // performs SQL UPDATE to write all of the changes the user made in datagridview1
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

        // performs a single update command pulled from the queryTextBox when the updateButton is clicked
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

        public async void Query1(object objTable, object objSchema)
        {


            try
            {
                string table = objTable as string;

                string schema = objSchema as string;

                string query = $"SELECT * FROM {schema}.{table};";

                Console.WriteLine($"Query = {query}");

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

                for (int index = 0; index < rdr.FieldCount; index++)
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

        // performs query typed in queryTextBox when the queryButton is clicked
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
