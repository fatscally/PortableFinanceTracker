using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.IO;


namespace PFT.Data
{
    public class SQLiteUtilities
    {


#region "Table definitions"
        //START OK FOR SQLITE
        public void CreateTransactionsTable(bool dropTable)
        {
            if (dropTable)
                this.dropTable("Transactions");

            createTable("CREATE TABLE IF NOT EXISTS Transactions ("
              + "Id INTEGER PRIMARY KEY, "
              + "ItemId INTEGER NOT NULL, "
              + "Price REAL NOT NULL, "
              + "PaymentTypeId INTEGER NULL, "
              + "IsIncome INTEGER NULL, "
              + "Comment TEXT NULL, "
              + "SupplierId INTEGER NULL, "
              + "InsertType TEXT NULL, "
              + "Date datetime NOT NULL,"
              + "ReqdSpend INTEGER NULL)");   
         }

        public void CreateTransactionAutoRepeatTable(bool dropTable)
        {
            if (dropTable)
                this.dropTable("TransactionAutoRepeat");

            createTable("CREATE TABLE IF NOT EXISTS TransactionAutoRepeat ("
              + "Id INTEGER PRIMARY KEY, "
              + "ItemId INTEGER NOT NULL, "
              + "Price REAL NOT NULL, "
              + "Comment TEXT NULL, "
              + "SupplierId INTEGER NULL, "
              + "RepeatTypeId INTEGER NULL, "
              + "RepeatDay INTEGER NULL)");
        }

        public void CreateItemsTable(bool dropTable)
        {

            if (dropTable)
                this.dropTable("Items");

            createTable("CREATE TABLE IF NOT EXISTS Items ("
              + "Id INTEGER PRIMARY KEY, "
              + "[Name] TEXT NOT NULL, "
              + "[Description] TEXT NULL, "
              + "[DefaultPrice] REAL NULL, "
              + "[Budget] REAL NULL, "
              + "[NeedIt] INTEGER NULL, "
              + "[IsIncome] INTEGER NULL, "
              + "[Hits] INTEGER NULL)");  //Number of times this row was used.
        }

        public void CreateTagsTable(bool dropTable)
        {
            if (dropTable)
                this.dropTable("Tags");

            createTable("CREATE TABLE IF NOT EXISTS Tags ("
              + "Id INTEGER PRIMARY KEY, "
              + "[Name] TEXT NOT NULL, "
              + "[Description] TEXT NULL, "
              + "[Budget] REAL NULL, "
              + "[ParentTagId] INTEGER NOT NULL DEFAULT -1, "
              + "[Hits] INTEGER NULL, "
              + "[SystemLockedRow] INTEGER NULL )");  //these rows are hard coded metadata.
        }

        public void CreateSuppliersTable(bool dropTable)
        {
            if (dropTable)
                this.dropTable("Suppliers");

            createTable("CREATE TABLE IF NOT EXISTS Suppliers ("
              + "Id INTEGER PRIMARY KEY, "
              + "[Name] TEXT NOT NULL, "
              + "[Description] TEXT NULL,"
              + "[Hits] INTEGER NULL )");
        }

        public void CreatePaymentTypesTable(bool dropTable)
        {
            if (dropTable)
                this.dropTable("PaymentTypes");

            createTable("CREATE TABLE IF NOT EXISTS PaymentTypes ("
              + "Id INTEGER PRIMARY KEY, "
              + "[Name] TEXT NOT NULL, "
              + "[Description] TEXT NULL, "
              + "[Hits] INTEGER NULL )");
        }

        public void CreateTransaction_TagsTable()
        {
            createTable("CREATE TABLE IF NOT EXISTS Transaction_Tags ("
              + "TransactionId INTEGER NOT NULL, "
              + "[TagId] INTEGER NOT NULL)");
        }

        public void CreateSettingsTable(bool dropTable)
        {
            if (dropTable)
                this.dropTable("Settings");

            createTable("CREATE TABLE IF NOT EXISTS Settings ("
              + "Id INTEGER PRIMARY KEY, "
              + "[Key] TEXT NOT NULL, "
              + "[Value] TEXT NULL )");
        }

        public void CreateItemDefaultTagsTable(bool dropTable)
        {
            if (dropTable)
                this.dropTable("ItemDefaultTags");

            createTable("CREATE TABLE IF NOT EXISTS ItemDefaultTags ("
              + "ItemId INTEGER NOT NULL, "
              + "TagId INTEGER NOT NULL"
              + " )");
        }

        public void CreateItemDefaultPaymentTypeTable(bool dropTable)
        {
            if (dropTable)
                this.dropTable("ItemDefaultPaymentType");

            createTable("create table ItemDefaultPaymentType ("
              + "ItemId INTEGER NOT NULL, "
              + "PaymentTypeId INTEGER NOT NULL"
              + " )");
        }

        public void CreateRepeatTypesTable(bool dropTable)
        {
            if (dropTable)
                this.dropTable("RepeatTypes");

            createTable("create table RepeatTypes ("
              + "Id INTEGER PRIMARY KEY, "
              + "[Name] TEXT NOT NULL)");
        }

        public void CreateDateRanges(bool dropTable)
        {
            if (dropTable)
                this.dropTable("DateRanges");

            createTable("CREATE TABLE IF NOT EXISTS DateRanges ("
              + "Id INTEGER PRIMARY KEY, "
              + "[Name] TEXT NOT NULL, "
              + "[Description] TEXT NULL )");
        }
        //END OK FOR SQLITE  didn't go past here.

        /// <summary>
        /// How many times an item was bought at a supplier
        /// </summary>
        internal void CreateItemSupplierMatchCountTable()
        {
            dropTable("ItemSupplierMatchCount");

            createTable("create table ItemSupplierMatchCount ("
              + "ItemId INTEGER NOT NULL, "
              + "SupplierId INTEGER NOT NULL, "
              + "PurchaseCount INTEGER NOT NULL)");
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
        public void InsertRepeatTypesRows()
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name) Values('Weekly')";
                cmd.CommandText += ", ('Monthly')";
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
        public void InsertPaymentTypesRows()
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name), (Description) Values('Cash','Cash money')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name), (Description) Values('Credit Card','Credit Card')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name), (Description) Values('Direct Debit','Direct Debit')";
                cmd.ExecuteNonQuery();
                cmd.CommandText = "INSERT INTO RepeatTypes (Name), (Description) Values('Cheque','Cheque')";
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
        public void Insert_Tags_Rows()
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();

                cmd.CommandText = "INSERT INTO Tags (Id, Name, Description, Budget, ParentTagId, Hits, SystemLockedRow ) Values";
                cmd.CommandText += "  (1, 'Home & Utilities','Running the home', 0, 0, 0, 1)";
                cmd.CommandText += ", (2, 'Food','Everything you eat', 0, 0, 0, 1)";
                cmd.CommandText += ", (3, 'Transport','Everything Transport related', 0, 0, 0, 1)";  //2
                cmd.CommandText += ", (4, 'Personal','Spent just on you', 0, 0, 0, 1)"; //3
                cmd.CommandText += ", (5, 'Children','Everything for the children', 0, 0, 0, 1)"; //4
                cmd.CommandText += ", (6, 'Health','Doctors, dentist, medicines...', 0, 0, 0, 1)"; //4
                cmd.CommandText += ", (7, 'Entertainment','Cinema, socialising etc', 0, 0, 0, 1)"; //4
                

                // 1 Utilities Essential
                cmd.CommandText += ", (null, 'Mortgage or Rent','Payments for your home', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Electricity','Electricity for your home', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Gas','Gas for your home', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Heating Oil','Home heating oil', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Water','Water for your home', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Refuse','Refuse collection service for your home', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Home Taxes','Property or council tax etc', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Home Insurance','Home & Contents insurance', 0, 1, 0, 1)";
                //Utilities non-Essential
                cmd.CommandText += ", (null, 'Cable TV','Television services', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Internet','Internet services', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Land Line Phone','Telephone services', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Credit Card Interest','Credit card fees and interest', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Banking Fees','Credit card fees and interest', 0, 1, 0, 1)";
                
                //Home and Garden
                cmd.CommandText += ", (null, 'Furniture','Beds, tables, chairs...', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Appliances','Cookers, refridgerator...', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Fixings','Bath, sink, door bell...', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Maintenance','Repairs', 0, 1, 0, 1)";
                cmd.CommandText += ", (null, 'Gardening','Flowers, shrubs, plant food', 0, 1, 0, 1)";

                

                // 2 Food
                cmd.CommandText += ", (null, 'Groceries','Food for your fridge and cupboard', 0, 2, 0, 1)";
                cmd.CommandText += ", (null, 'Food at work','Food bought at work', 0, 2, 0, 1)";
                cmd.CommandText += ", (null, 'Restaurant','Dining out', 0, 2, 0, 1)";
                cmd.CommandText += ", (null, 'Fast food & Take out','Fast food & Take out food', 0, 2, 0, 1)";
                cmd.CommandText += ", (null, 'Coffee n beverages','Your favourite cuppa', 0, 2, 0, 1)";
                cmd.CommandText += ", (null, 'Snacks','Your favourite cuppa', 0, 2, 0, 1)";
                //cmd.CommandText += ", (null, 'Coffee','Take out coffees', 0, 2, 0, 1)";


                // 3 Transport
                cmd.CommandText += ", (null, 'Car Fuel','Petrol, diesel, gasoline...', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Car Repayments','Car Insurance', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Car Insurance','Car Insurance', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Car Tax','Car Insurance', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Fines','Penalties & Fines', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Road Tolls','', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Car Parks','', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Car Repair & Maintenance','Oil, tyres, road worthy certification', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Car accessories','Air freshener, alloy wheels...', 0, 3, 0, 1)";

                cmd.CommandText += ", (null, 'Travel Card','Leap card, Oyster card, Metro card...', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Bicycle','Bicycle & Accessories', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Bus','Bus Fares', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Tram','', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Train','', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Metro','', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Flights','', 0, 3, 0, 1)";
                cmd.CommandText += ", (null, 'Ferries','', 0, 3, 0, 1)";

                // 4 Personal
                cmd.CommandText += ", (null, 'Hair care','Hair dresser, barber, products', 0, 4, 0, 1)";
                cmd.CommandText += ", (null, 'Nails','Manicure, pedicure', 0, 4, 0, 1)";
                cmd.CommandText += ", (null, 'Clothes','Shirts, shoes, dresses...', 0, 4, 0, 1)";
                cmd.CommandText += ", (null, 'Education','Books, training classes', 0, 4, 0, 1)";
                cmd.CommandText += ", (null, 'Sport personal','Club memberships, equipment and clothing.', 0, 4, 0, 1)";
                cmd.CommandText += ", (null, 'Magazines & News','Newspapers, magazines', 0, 4, 0, 1)";
                cmd.CommandText += ", (null, 'Tech','Tech toys; iPads, watches', 0, 4, 0, 1)";
                cmd.CommandText += ", (null, 'Jewellry','bling bling', 0, 4, 0, 1)";
                
                cmd.CommandText += ", (null, 'Gifts','Presents & celebrations', 0, 4, 0, 1)";
                cmd.CommandText += ", (null, 'Charities','Shirts, shoes, dresses...', 0, 4, 0, 1)";
                cmd.CommandText += ", (null, 'Clothes','Shirts, shoes, dresses...', 0, 4, 0, 1)";
                cmd.CommandText += ", (null, 'Clothes','Shirts, shoes, dresses...', 0, 4, 0, 1)";


                // 5 Children
                cmd.CommandText += ", (null, 'Clothes','Shirts, shoes, dresses...', 0, 5, 0, 1)";
                cmd.CommandText += ", (null, 'Toys','', 0, 5, 0, 1)";
                cmd.CommandText += ", (null, 'Child Support','Child maintenance payments', 0, 5, 0, 1)";
                cmd.CommandText += ", (null, 'Child care','Créche, Kintergarten, Play school', 0, 5, 0, 1)";
                cmd.CommandText += ", (null, 'School costs','Fees, uniforms, excursions', 0, 5, 0, 1)";
                cmd.CommandText += ", (null, 'Baby sitting','', 0, 5, 0, 1)";
                cmd.CommandText += ", (null, 'Pocket Money','', 0, 5, 0, 1)";
                cmd.CommandText += ", (null, 'Sport & Extra Curriulum','Extra curriculum classes', 0, 5, 0, 1)";
                cmd.CommandText += ", (null, 'Baby furniture','Baths, changing tables', 0, 5, 0, 1)";


                // 6 Health
                cmd.CommandText += ", (null, 'Doctor','GP', 0, 6, 0, 1)";
                cmd.CommandText += ", (null, 'Medicines','Pills, ointments, sprays', 0, 6, 0, 1)";
                cmd.CommandText += ", (null, 'Dentist','', 0, 6, 0, 1)";
                cmd.CommandText += ", (null, 'Physio','', 0, 6, 0, 1)";
                cmd.CommandText += ", (null, 'Hospital','', 0, 6, 0, 1)";

                // 7 Entertainment & Social
                cmd.CommandText += ", (null, 'Cinema','Lotto, scratch cards, betting', 0, 7, 0, 1)";
                cmd.CommandText += ", (null, 'Movie Rentals','', 0, 7, 0, 1)";
                cmd.CommandText += ", (null, 'Concerts & Shows','', 0, 7, 0, 1)";
                cmd.CommandText += ", (null, 'Alcohol & Socialising','Pubs, clubs & wine', 0, 7, 0, 1)";
                cmd.CommandText += ", (null, 'Gambling','Lotto, scratch cards, betting', 0, 7, 0, 1)";

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
        public void Insert_DateRanges_Rows()
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();

                cmd.CommandText = "INSERT INTO DateRanges (Name, Description) Values('Today','Today')";
                cmd.CommandText += ", ('Since Monday','Since Monday')";
                cmd.CommandText += ", ('Last 7 Days','Last 7 Days')";
                cmd.CommandText += ", ('This Month','This Month')";
                cmd.CommandText += ", ('Last 28 Days','Last 28 Days')";
                cmd.CommandText += ", ('This Year','This Year')";
                cmd.ExecuteNonQuery();
                
            }
            catch
            {
                throw;
            }
        }


        #endregion





        private string createTable(string sql)
        {
            //OK for SQLite
            PFT.Data.Connection conn = new PFT.Data.Connection();
            SQLiteCommand cmd;
            

            try
            {
                cmd = new SQLiteCommand(sql, conn.LocalConnection());
                cmd.ExecuteNonQuery();
                return "Table Created";
            }
            catch (SQLiteException sqlexception)
            {
                return sqlexception.Message;
            } 
            catch (Exception ex)
            {
                return ex.Message;
            }
            finally
            {
                conn.LocalConnection().Close();
            }
        }

        private string dropTable(string tableName)
        {
            PFT.Data.Connection conn = new PFT.Data.Connection();
            SQLiteCommand cmd;

            using (SQLiteConnection exConnection = conn.LocalConnection())
            {
                //if (exConnection.TableExists(tableName))
                //{
                    try
                    {
                        //cmd = new SQLiteCommand(sql, conn.LocalConnection());
                        string sql = "DROP TABLE " + tableName;
                        cmd = new SQLiteCommand(sql, conn.LocalConnection());
                        cmd.ExecuteNonQuery();
                        return tableName + " dropped.";
                    }
                    catch (SQLiteException sqlexception)
                    {
                        return sqlexception.Message;
                    }
                //}
                //return "No connection to database found.";
            }
        }


        //Upgrades SQLCE database to version 4.0
        //public void UpgradeCEDatabase()
        //{
        //    string filename = @"C:\Documents and Settings\RBRENNAN\My Documents\visual studio 2010\Projects\PFT\PFT\PTF.sdf";
        //    var engine = new System.Data.SqlServerCe.SqlCeEngine("Data Source=" + filename);
        //    engine.EnsureVersion40(filename);
        //}

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
    

    //public static class SqlCeUpgrade
    //{
    //    public static void EnsureVersion40(this System.Data.SqlServerCe.SqlCeEngine engine, string filename)
    //    {
    //        SQLCEVersion fileversion = DetermineVersion(filename);
    //        if (fileversion == SQLCEVersion.SQLCE20)
    //            throw new ApplicationException("Unable to upgrade from 2.0 to 4.0");

    //        if (SQLCEVersion.SQLCE40 > fileversion)
    //        {
    //            engine.Upgrade();
    //        }
    //    }
    //    private enum SQLCEVersion
    //    {
    //        SQLCE20 = 0,
    //        SQLCE30 = 1,
    //        SQLCE35 = 2,
    //        SQLCE40 = 3
    //    }
    //    private static SQLCEVersion DetermineVersion(string filename)
    //    {
    //        var versionDictionary = new Dictionary<int, SQLCEVersion> 
    //    { 
    //        { 0x73616261, SQLCEVersion.SQLCE20 }, 
    //        { 0x002dd714, SQLCEVersion.SQLCE30},
    //        { 0x00357b9d, SQLCEVersion.SQLCE35},
    //        { 0x003d0900, SQLCEVersion.SQLCE40}
    //    };
    //        int versionLONGWORD = 0;
    //        try
    //        {
    //            using (var fs = new FileStream(filename, FileMode.Open))
    //            {
    //                fs.Seek(16, SeekOrigin.Begin);
    //                using (BinaryReader reader = new BinaryReader(fs))
    //                {
    //                    versionLONGWORD = reader.ReadInt32();
    //                }
    //            }
    //        }
    //        catch
    //        {
    //            throw;
    //        }
    //        if (versionDictionary.ContainsKey(versionLONGWORD))
    //        {
    //            return versionDictionary[versionLONGWORD];
    //        }
    //        else
    //        {
    //            throw new ApplicationException("Unable to determine database file version");
    //        }
    //    }


    //}



}
