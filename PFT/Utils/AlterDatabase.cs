using System;
using System.Collections.Generic;
using System.Data;
//using System.Data.SqlServerCe;
using System.Data.SQLite;
using System.IO;
using System.Windows;


namespace PFT.Utils
{

    public class SQLiteUtilities
    {
        #region "Table definitions"

        internal void CreateTransactionsTable()
        {
            createTable("create table TransactionLite ("
              + "Id int PRIMARY KEY, "
              + "ItemId int NOT NULL, "
              + "Price money NOT NULL, "
              + "PaymentTypeId int NULL, "
              + "[IsIncome] bit NULL, "
              + "Comment nvarchar (255) NULL, "
              + "SupplierId int NULL, "
              + "InsertType char (1) NULL, "
              + "Date datetime NOT NULL, "
              + "PRIMARY KEY(Id) )");            
        }


        #endregion


        private void createTable(string sql)
        {

            PFT.Data.Connection conn = new PFT.Data.Connection();
            SQLiteCommand cmd;
            

            try
            {
                cmd = new SQLiteCommand(sql, conn.LocalConnection());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Table Created", "Table Created");
            }
            catch (SQLiteException sqlexception)
            {
                MessageBox.Show(sqlexception.Message, "SqlCeException");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception in createTable");
            }
            finally
            {
                conn.LocalConnection().Close();
            }
        }


    }


    public class SqlCeUtilities
    {


#region "Table definitions"

        internal void CreateTransactionsTable()
        {
            dropTable("Transactions");
            
            createTable("create table Transaction ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "ItemId int NOT NULL, "
              + "Price money NOT NULL, "
              + "PaymentTypeId int NULL, "
              + "[IsIncome] bit NULL, "
              + "Comment nvarchar (255) NULL, "
              + "SupplierId int NULL, "
              + "InsertType nchar (1) NOT NULL, "
              + "Date datetime NOT NULL, "
              + "PRIMARY KEY(Id) )");
         }

        internal void CreateTransactionAutoRepeatTable()
        {
            dropTable("TransactionAutoRepeat");

            createTable("create table TransactionAutoRepeat ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "ItemId int NOT NULL, "
              + "Price money NOT NULL, "
              + "Comment nvarchar (255) NULL, "
              + "SupplierId int NULL, "
              + "RepeatTypeId int NULL, "
              + "RepeatDay int NULL, "
              + "LastDateInserted datetime NULL, "
              + "PRIMARY KEY(Id) )");
        }

        internal void CreateItemsTable()
        {
            dropTable("Items");

            createTable("create table Items ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Name] nvarchar(50) NOT NULL, "
              + "[Description] nvarchar(255) NULL, "
              + "[DefaultPrice] money NULL, "
              + "[Budget] money NULL, "
              + "[JustWantIt] bit NULL, "
              + "[IsIncome] bit NULL, "
              + "PRIMARY KEY(Id) )");
        }

        internal void CreateTagsTable()
        {
            dropTable("Tags");

            createTable("create table Tags ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Name] nvarchar(50) NOT NULL, "
              + "[Description] nvarchar(255) NULL, "
              + "[ParentTagId] int NOT NULL DEFAULT -1, "
              + "PRIMARY KEY(Id) )");
        }

        internal void CreateSuppliersTable()
        {
            dropTable("Suppliers");

            createTable("create table Suppliers ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Name] nvarchar(50) NOT NULL, "
              + "[Description] nvarchar(255) NULL, "
              + "PRIMARY KEY(Id) )");
        }

        internal void CreatePaymentTypesTable()
        {
            dropTable("PaymentTypes");

            createTable("create table PaymentTypes ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Name] nvarchar(50) NOT NULL, "
              + "[Description] nvarchar(255) NULL, "
              + "PRIMARY KEY(Id) )");
        }

        internal void CreateTransaction_TagsTable()
        {
            dropTable("Transaction_Tags");

            createTable("create table Transaction_Tags ("
              + "TransactionId int NOT NULL, "
              + "[TagId] int NOT NULL)");
        }

        internal void CreateSettingsTable()
        {
            dropTable("Settings");

            createTable("create table Settings ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Key] nvarchar(50) NOT NULL, "
              + "[Value] nvarchar(100) NULL, "
              + "PRIMARY KEY(Id) )");
        }

        internal void CreateItemDefaultTagsTable()
        {
            dropTable("ItemDefaultTags");

            createTable("create table ItemDefaultTags ("
              + "ItemId int NOT NULL, "
              + "TagId int NOT NULL"
              + " )");
        }

        internal void CreateItemDefaultPaymentTypeTable()
        {
            dropTable("ItemDefaultPaymentType");

            createTable("create table ItemDefaultPaymentType ("
              + "ItemId int NOT NULL, "
              + "PaymentTypeId int NOT NULL"
              + " )");
        }

        internal void CreateRepeatTypesTable()
        {
            dropTable("RepeatTypes");

            createTable("create table RepeatTypes ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Name] nvarchar(50) NOT NULL, "
              + "PRIMARY KEY(Id) )");
        }

        internal void CreateDateRanges()
        {
            dropTable("DateRanges");

            createTable("create table DateRanges ("
              + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
              + "[Name] nvarchar(20) NOT NULL, "
              + "[Description] nvarchar(255) NULL, "
              + "PRIMARY KEY(Id) )");
        }


        /// <summary>
        /// How many times an item was bought at a supplier
        /// </summary>
        internal void CreateItemSupplierMatchCountTable()
        {
            dropTable("ItemSupplierMatchCount");

            createTable("create table ItemSupplierMatchCount ("
              + "ItemId int NOT NULL, "
              + "SupplierId int NOT NULL, "
              + "PurchaseCount int NOT NULL)");
        }


        //might want to do this.
        //internal void CreateTagGroupsTable()
        //{
        //    dropTable("TagGroups");

        //    createTable("create table Settings ("
        //      + "Id int NOT NULL IDENTITY(1,1) UNIQUE, "
        //      + "[Setting] nvarchar(50) NOT NULL, "
        //      + "[Value] nvarchar(100) NULL, "
        //      + "PRIMARY KEY(Id) )");
        //}

#endregion  

        #region"Metadata Inserts"


        
        /// <summary>
        /// Insert payment repeat types into RepeatTypes table
        /// </summary>
        internal void InsertRepeatTypesRows()
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name) Values('Weekly')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name) Values('Monthly')";
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Insert payment types Metadata into PaymentTypes table
        /// </summary>
        internal void InsertPaymentTypesRows()
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO PaymentTypes (Name), (Description) Values('Cash','Cash money')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO PaymentTypes (Name), (Description) Values('Credit Card','Credit Card')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO PaymentTypes (Name), (Description) Values('Direct Debit','Direct Debit')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO PaymentTypes (Name), (Description) Values('Cheque','Cheque')";
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Insert Date Ranges for searching into DateRanges table
        /// </summary>
        internal void InsertDateRangesRows()
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO DateRanges (Name), (Description) Values('Today','Today')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO DateRanges (Name), (Description) Values('SinceMonday','Since Monday')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO DateRanges (Name), (Description) Values('Last7Days','Last 7 Days')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO DateRanges (Name), (Description) Values('ThisMonth','This Month')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO DateRanges (Name), (Description) Values('Last28Days','Last 28 Days')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO DateRanges (Name), (Description) Values('ThisYear','This Year')";
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        #endregion





        private void createTable(string sql)
        {

            PFT.Data.Connection conn = new PFT.Data.Connection();
            SQLiteCommand cmd;
            

            try
            {
                cmd = new SQLiteCommand(sql, conn.LocalConnection());
                cmd.ExecuteNonQuery();
                MessageBox.Show("Table Created", "Table Created");
            }
            catch (SQLiteException sqlexception)
            {
                MessageBox.Show(sqlexception.Message, "SqlCeException");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Exception in createTable");
            }
            finally
            {
                conn.LocalConnection().Close();
            }
        }

        private void dropTable(string tableName)
        {
            PFT.Data.Connection conn = new PFT.Data.Connection();
            SQLiteCommand cmd;

            using (SQLiteConnection exConnection = conn.LocalConnection())
            {
                if (exConnection.TableExists(tableName))
                {
                    try
                    {
                        //cmd = new SQLiteCommand(sql, conn.LocalConnection());
                        string sql = "drop table " + tableName;
                        cmd = new SQLiteCommand(sql, conn.LocalConnection());
                        cmd.ExecuteNonQuery();
                        Console.WriteLine(tableName + " Table dropped.");
                    }
                    catch (SQLiteException sqlexception)
                    {
                        MessageBox.Show(sqlexception.Message, "Drop table error");
                    }
                }
            }
        }



        internal void UpgradeDatabase()
        {
            string filename = @"C:\Documents and Settings\RBRENNAN\My Documents\visual studio 2010\Projects\PFT\PFT\PTF.sdf";
            var engine = new System.Data.SqlServerCe.SqlCeEngine("Data Source=" + filename);
            engine.EnsureVersion40(filename);
        }

    }



    public static class SqlCeExtentions
    {
        public static bool TableExists(this SQLiteConnection connection, string tableName)
        {
            if (tableName == null) throw new ArgumentNullException("tableName");
            if (string.IsNullOrWhiteSpace(tableName)) throw new ArgumentException("Invalid table name");
            if (connection == null) throw new ArgumentNullException("connection");
            if (connection.State != ConnectionState.Open)
            {
                throw new InvalidOperationException("TableExists requires an open and available Connection. The connection's current state is " + connection.State);
            }

            using (SQLiteCommand command = connection.CreateCommand())
            {
                command.CommandType = CommandType.Text;
                command.CommandText = "SELECT 1 FROM Information_Schema.Tables WHERE TABLE_NAME = @tableName";
                command.Parameters.AddWithValue("tableName", tableName);
                object result = command.ExecuteScalar();
                return result != null;
            }
        }
    }
    

    public static class SqlCeUpgrade
    {
        public static void EnsureVersion40(this System.Data.SqlServerCe.SqlCeEngine engine, string filename)
        {
            SQLCEVersion fileversion = DetermineVersion(filename);
            if (fileversion == SQLCEVersion.SQLCE20)
                throw new ApplicationException("Unable to upgrade from 2.0 to 4.0");

            if (SQLCEVersion.SQLCE40 > fileversion)
            {
                engine.Upgrade();
            }
        }
        private enum SQLCEVersion
        {
            SQLCE20 = 0,
            SQLCE30 = 1,
            SQLCE35 = 2,
            SQLCE40 = 3
        }
        private static SQLCEVersion DetermineVersion(string filename)
        {
            var versionDictionary = new Dictionary<int, SQLCEVersion> 
        { 
            { 0x73616261, SQLCEVersion.SQLCE20 }, 
            { 0x002dd714, SQLCEVersion.SQLCE30},
            { 0x00357b9d, SQLCEVersion.SQLCE35},
            { 0x003d0900, SQLCEVersion.SQLCE40}
        };
            int versionLONGWORD = 0;
            try
            {
                using (var fs = new FileStream(filename, FileMode.Open))
                {
                    fs.Seek(16, SeekOrigin.Begin);
                    using (BinaryReader reader = new BinaryReader(fs))
                    {
                        versionLONGWORD = reader.ReadInt32();
                    }
                }
            }
            catch
            {
                throw;
            }
            if (versionDictionary.ContainsKey(versionLONGWORD))
            {
                return versionDictionary[versionLONGWORD];
            }
            else
            {
                throw new ApplicationException("Unable to determine database file version");
            }
        }


    }



}
