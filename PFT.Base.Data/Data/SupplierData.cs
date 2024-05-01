using PFT.Base;
using System.Data.SQLite;
using PFT.Interfaces;
using System;

namespace PFT.Data
{
    public class SupplierData : ISupplierData
    {
        public void Save(Supplier supplier)
        {
            int supplierId = supplier.Id;

            //"If Exists" isn't used in SqlCe so I have to do a separate SELECT
            //if (supplierId <= 0)
            //    supplierId = Select(supplier).Id;

            //if it is still <=0 after the select then INSERT
            if (supplierId <= 0)
                insert(supplier);
            else
                Update(supplier);
        }

        public Supplier Select(Supplier supplier)
        {

            if (supplier.Name == "")
                return supplier;

            return Select(supplier.Id);

            //try
            //{
            //    SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
            //    cmd.CommandText = "SELECT * FROM Suppliers WHERE Id=" + supplier.Id;

            //    SQLiteDataReader reader = cmd.ExecuteReader();

            //    while (reader.Read())
            //    {
            //        supplier.Id = int.Parse(reader["Id"].ToString());
            //        supplier.Name = reader["Name"].ToString();
            //        supplier.Description = reader["Description"].ToString();
            //    }

            //    return supplier;
            //}
            //catch
            //{
            //    throw;
            //}

        }

        public Supplier Select(int supplierId)
        {

            try
            {
                Supplier supplier = new Supplier();

                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Suppliers WHERE Id=" + supplierId;

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    supplier.Id = int.Parse(reader["Id"].ToString());
                    supplier.Name = reader["Name"].ToString();
                    supplier.Description = reader["Description"].ToString();
                }

                return supplier;
            }
            catch
            {
                throw;
            }

        }



        private void insert(Supplier supplier)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO Suppliers (Name, Description) Values('" + supplier.Name + "', '" + supplier.Description + "')";

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        private void Update(Supplier supplier)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "UPDATE Suppliers Set Name='" + supplier.Name + "', Description='" + supplier.Description + "'  WHERE Id = " + supplier.Id;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void Delete(Supplier supplier)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "DELETE FROM Suppliers WHERE Id = " + supplier.Id;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Gets the total of all money spent with the supplier for the given date range.
        /// </summary>
        /// <param name="startDate">Start date of period to calculate</param>
        /// <param name="endDate">End date of period to calculate</param>
        /// /// <param name="isIncome">True if calculating incomes</param>
        /// <returns>Sum of money spent </returns>
        public double GetSupplierSpendTotal(int supplierId, DateTime startDate, DateTime endDate, bool isIncome)
        {
            TransactionCol returnCol = new TransactionCol();
            string strCmdText;


            string strIsIncome = "0";  //stupid SQLCE bug won't handle True/False
            if (isIncome)
                strIsIncome = "1";

            
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                strCmdText = "SELECT SUM(Price) AS TotalSpend FROM Transactions ";
                strCmdText += "WHERE SupplierId = " + supplierId + " AND IsIncome = " + strIsIncome;
                strCmdText += " AND Date Between '" + startDate.ToString("yyyy-MM-dd") + " 00:00:00:001' AND '" + endDate.ToString("yyyy-MM-dd ") + "23:59:59'";
                //Sqlite uses dashes "yyyy-MM-dd" not "yyyy/MM/dd"
                cmd.CommandText = strCmdText;
                object rtnVal = cmd.ExecuteScalar();

                double outNumber;
                double.TryParse(rtnVal.ToString(), out outNumber);

                return outNumber;

            }
            catch
            {
                throw;
            }
        }


    }
}
