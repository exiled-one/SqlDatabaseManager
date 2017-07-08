using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tester
{
    public class TableSchema
    {
        public TableSchema(string connectionString)
        {
            this.TableList = new List<string>();
            this.ColumnList = new List<Columns>();
            this.PrimaryKeyList = new List<PrimaryKey>();
            this.ForeignKeyList = new List<ForeignKey>();
            this.UniqueKeyList = new List<UniqueKey>();

            GetDataBaseSchema(connectionString);
            ShowDataBaseSchema();

        }

        public List<string> TableList { get; set; }
        public List<Columns> ColumnList { get; set; }
        public List<PrimaryKey> PrimaryKeyList { get; set; }
        public List<UniqueKey> UniqueKeyList { get; set; }
        public List<ForeignKey> ForeignKeyList { get; set; }

        private void ShowDataBaseSchema()
        {
            Console.WriteLine();
            Console.WriteLine("TableList");
            Console.WriteLine();
            foreach (string s in TableList)
            {
                Console.WriteLine(s);
            }
            Console.WriteLine();
            Console.WriteLine("ColumnList");
            Console.WriteLine("--------------------------------------------------------");
            foreach (Columns c in ColumnList)
            {
                Console.WriteLine();
                Console.WriteLine(c.TableName);
                Console.WriteLine(c.FieldName);
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("PrimaryKeyList");
            Console.WriteLine("--------------------------------------------------------");
            foreach (PrimaryKey pk in PrimaryKeyList)
            {
                Console.WriteLine();
                Console.WriteLine(pk.TableName);
                Console.WriteLine(pk.FieldName);
                Console.WriteLine(pk.PrimaryKeyName);
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("UniqueKeyList");
            Console.WriteLine("--------------------------------------------------------");
            foreach (UniqueKey uk in UniqueKeyList)
            {
                Console.WriteLine();
                Console.WriteLine(uk.TableName);
                Console.WriteLine(uk.FieldName);
                Console.WriteLine(uk.UniqueKeyName);
                Console.WriteLine();
            }
            Console.WriteLine();
            Console.WriteLine("ForeignKeyList");
            Console.WriteLine("--------------------------------------------------------");
            foreach (ForeignKey fk in ForeignKeyList)
            {
                Console.WriteLine();
                Console.WriteLine(fk.TableName);
                Console.WriteLine(fk.ForeignName);
                Console.WriteLine();

            }
        }
        protected void GetDataBaseSchema(string ConnectionString)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {

                System.Data.SqlClient.SqlConnectionStringBuilder builder = new System.Data.SqlClient.SqlConnectionStringBuilder();
                builder.ConnectionString = ConnectionString;
                string server = builder.DataSource;
                string database = builder.InitialCatalog;

                connection.Open();


                DataTable schemaTables = connection.GetSchema("Tables");

                foreach (System.Data.DataRow rowTable in schemaTables.Rows)
                {
                    String tableName = rowTable.ItemArray[2].ToString();
                    this.TableList.Add(tableName);

                    string[] restrictionsColumns = new string[4];
                    restrictionsColumns[2] = tableName;
                    DataTable schemaColumns = connection.GetSchema("Columns", restrictionsColumns);

                    foreach (System.Data.DataRow rowColumn in schemaColumns.Rows)
                    {
                        string ColumnName = rowColumn[3].ToString();
                        this.ColumnList.Add(new Columns() { TableName = tableName, FieldName = ColumnName });
                    }

                    string[] restrictionsPrimaryKey = new string[4];
                    restrictionsPrimaryKey[2] = tableName;
                    DataTable schemaPrimaryKey = connection.GetSchema("IndexColumns", restrictionsColumns);


                    foreach (System.Data.DataRow rowPrimaryKey in schemaPrimaryKey.Rows)
                    {
                        string indexName = rowPrimaryKey[2].ToString();

                        if (indexName.IndexOf("PK_") != -1)
                        {
                            this.PrimaryKeyList.Add(new PrimaryKey()
                            {
                                TableName = tableName,
                                FieldName = rowPrimaryKey[6].ToString(),
                                PrimaryKeyName = indexName
                            });
                        }

                        if (indexName.IndexOf("UQ_") != -1)
                        {
                            this.UniqueKeyList.Add(new UniqueKey()
                            {
                                TableName = tableName,
                                FieldName = rowPrimaryKey[6].ToString(),
                                UniqueKeyName = indexName
                            });
                        }

                    }


                    string[] restrictionsForeignKeys = new string[4];
                    restrictionsForeignKeys[2] = tableName;
                    DataTable schemaForeignKeys = connection.GetSchema("ForeignKeys", restrictionsColumns);


                    foreach (System.Data.DataRow rowFK in schemaForeignKeys.Rows)
                    {

                        this.ForeignKeyList.Add(new ForeignKey()
                        {
                            ForeignName = rowFK[2].ToString(),
                            TableName = tableName,
                            // FieldName = rowFK[6].ToString() //There is no information
                        });
                    }


                }


            }

        }

    }

    public class Columns
    {
        public string TableName { get; set; }
        public string FieldName { get; set; }
    }

    public class PrimaryKey
    {
        public string TableName { get; set; }
        public string PrimaryKeyName { get; set; }
        public string FieldName { get; set; }
    }


    public class UniqueKey
    {
        public string TableName { get; set; }
        public string UniqueKeyName { get; set; }
        public string FieldName { get; set; }
    }

    public class ForeignKey
    {
        public string TableName { get; set; }
        public string ForeignName { get; set; }
        // public string FieldName { get; set; } //There is no information
    }
}