using System;
using System.Data.SqlServerCe;
using PFT.Base;
using PFT.Interfaces;

namespace PFT.Data
{
    internal class TransactionData : ITransactionData
    {

        public void Save(Transaction transaction)
        {
            int transactionId = transaction.Id;

            //"If Exists" isn't used in SqlCe so I have to do a separate SELECT
            if (transactionId <= 0)
                transactionId = Select(transaction).Id;

            //if it is still <=0 after the select then INSERT
            if (transactionId <= 0)
                transactionId = insert(transaction);
            else
            {
                update(transaction);
            }

            
            //save the Tags for the transaction
            ITagColData tcd = new TagColData();
            tcd.SaveAllTransaction_Tags(transactionId, transaction.Tags);

        }

        public Transaction Select(Transaction transaction)
        {
            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Transactions WHERE Id=" + transaction.Id;

                SqlCeDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    transaction.Id = int.Parse(reader["Id"].ToString());
                    transaction.Item.Id = int.Parse(reader["ItemId"].ToString());
                    transaction.Price = float.Parse(reader["Price"].ToString());
                    transaction.PaymentType.Id = int.Parse(reader["PaymentTypeId"].ToString());
                    transaction.IsIncome = bool.Parse(reader["IsIncome"].ToString());
                    transaction.Comment = reader["Comment"].ToString();
                    transaction.Supplier.Id = int.Parse(reader["SupplierId"].ToString());
                    transaction.InsertType = reader["InsertType"].ToString();
                    transaction.Date = DateTime.Parse(reader["Date"].ToString());
                }


                //get the tags collection for this transaction
                TagColData tcd = new TagColData();
                transaction.Tags = tcd.LoadWithTagsByTransactionId(transaction.Id);



                return transaction;
            }
            catch
            {
                throw;
            }

        }



        /// <summary>
        /// 
        /// </summary>
        /// <param name="transaction"></param>
        /// <returns>@@Identity</returns>
        private int insert(Transaction transaction)
        {
            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO Transactions (ItemId, Price, PaymentTypeId, IsIncome, Comment, SupplierId, InsertType, [Date])";
                cmd.CommandText += "Values('" + transaction.Item.Id + "', '" + transaction.Price + "', '" + transaction.PaymentType.Id + "', '" + transaction.IsIncome + "', '" + transaction.Comment + "', '" + transaction.Supplier.Id + "', '" + transaction.InsertType + "', '" + transaction.Date.ToString("yyyy-MM-dd HH:mm:ss.fff") + "')";

                cmd.ExecuteNonQuery();

                cmd.CommandText = "SELECT @@IDENTITY";
                return Convert.ToInt32(cmd.ExecuteScalar());
            }
            catch
            {
                throw;
            }
        }

        private void update(Transaction transaction)
        {
            try
            {
             
                int bitValue = 0;  //SqlCe problem??? Must convert FALSE to -1
                if (transaction.IsIncome)
                    bitValue = -1;

                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "UPDATE Transactions Set ItemId = " + transaction.Item.Id + ", Price = " + transaction.Price + ", PaymentTypeId = " + transaction.PaymentType.Id + ", IsIncome = " + bitValue + ", Comment = N'" + transaction.Comment + "', SupplierId = " + transaction.Supplier.Id + ", InsertType = N'" + transaction.InsertType + "', [Date] = '" + transaction.Date.ToString("yyyy-MM-dd HH:mm:ss.fff") + "' WHERE Id = " + transaction.Id;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void Delete(Transaction transaction)
        {
            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "DELETE FROM Transactions WHERE Id = " + transaction.Id;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


    }
}
