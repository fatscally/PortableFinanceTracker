using System;
using System.Data.SQLite;
using PFT.Base;
using PFT.Interfaces;

namespace PFT.Data
{
    public class TransactionColData : ITransactionColData
    {

        /// <summary>
        /// 
        /// </summary>
        /// <param name="startDate"></param>
        /// <param name="endDate"></param>
        /// <returns></returns>
        public TransactionCol LoadAll(DateTime startDate, DateTime endDate)
        {
            TransactionCol returnCol = new TransactionCol();
            string strCmdText;
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                strCmdText = "SELECT * FROM Transactions ";
                //strCmdText += "WHERE Date>= CONVERT(DATETIME, '" + startDate.ToString("yyyy/MM/dd") + " 00:00:00') AND Date<= CONVERT(DATETIME, '" + endDate.ToString("yyyy/MM/dd ") + "23:59:59')";
                strCmdText += "WHERE [Date] Between '" + startDate.ToString("yyyy-MM-dd") + " 00:00:00:001' AND '" + endDate.ToString("yyyy-MM-dd") + " 23:59:59'";
                strCmdText += " ORDER BY [Date] DESC";

                cmd.CommandText = strCmdText;
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Transaction transaction = new Transaction();
                    transaction.Id = int.Parse(reader["Id"].ToString());
                    transaction.Item.Id = int.Parse(reader["ItemId"].ToString());
                    transaction.Price = float.Parse(reader["Price"].ToString());
                    transaction.PaymentType.Id = int.Parse(reader["PaymentTypeId"].ToString());
                    transaction.IsIncome = bool.Parse(reader["IsIncome"].ToString());
                    transaction.Date = DateTime.Parse(reader["Date"].ToString());
                    transaction.Comment = reader["Comment"].ToString();
                    transaction.Supplier.Id = int.Parse(reader["SupplierId"].ToString());
                    transaction.InsertType = reader["InsertType"].ToString();

                    returnCol.Add(transaction);
                }


                //populate the Item for each transaction
                IItemData itemdata = new ItemData();
                ISupplierData supplierdata = new SupplierData();
                ITagColData tagcoldata = new TagColData();
                IPaymentTypeData ptData = new PaymentTypeData();
                foreach (Transaction t in returnCol)
                {
                    t.Item = itemdata.Select(t.Item.Id);
                    t.Supplier = supplierdata.Select(t.Supplier.Id);
                    t.Tags = tagcoldata.LoadWithTagsByTransactionId(t.Id);
                    t.PaymentType = ptData.Select(t.PaymentType.Id);
                }


                return returnCol;

            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// Sums the total money of all transactions for a given date range.
        /// </summary>
        /// <param name="startDate">Start date of period to calculate</param>
        /// <param name="endDate">End date of period to calculate</param>
        /// /// <param name="isIncome">True if calculating incomes</param>
        /// <returns>Sum of money spent </returns>
        public double GetTransactionBalance(DateTime startDate, DateTime endDate, bool isIncome)
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
                strCmdText += "WHERE IsIncome = " + strIsIncome;
                strCmdText += " AND Date>='" + startDate.ToString("yyyy-MM-dd") + " 00:00:00:000' AND '" + endDate.ToString("yyyy-MM-dd ") + "23:59:59'";


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
