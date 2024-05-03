using PFT.Interfaces;
using System.Data.SQLite;
using PFT.Base;

namespace PFT.Data
{
    public class Transaction_Tags_Data : ITransaction_Tags_Data
    {
        public void Save(Transaction_Tag transactionTag)
        {

            insert(transactionTag);

        }


        public Transaction_Tag Select(Transaction_Tag transactionTag)
        {

            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Transaction_Tags WHERE TransactionId=" + transactionTag.TransactionId + " AND TagId=" + transactionTag.TagId;

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    transactionTag.TransactionId = int.Parse(reader["TransactionId"].ToString());
                    transactionTag.TagId = int.Parse(reader["TagId"].ToString());
                }

                return transactionTag;
            }
            catch
            {
                throw;
            }
        }

        private void insert(Transaction_Tag transactionTag)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO Transaction_Tags (TransactionId, TagId) Values('" + transactionTag.TransactionId+ "', '" + transactionTag.TagId + "')";

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }
        public void Delete(Transaction_Tag transactionTag)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "DELETE FROM Transaction_Tags WHERE TransactionId=" + transactionTag.TransactionId + " AND TagId=" + transactionTag.TagId;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }



        /// <summary>
        /// This will clear all Tags for a Transaction
        /// </summary>
        /// <param name="transactionId"></param>
        public void DeleteTagsByTransactionId(int transactionId)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "DELETE FROM Transaction_Tags WHERE TransactionId=" + transactionId;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }



    }
}
