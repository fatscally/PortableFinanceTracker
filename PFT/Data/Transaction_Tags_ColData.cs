//using System.Data.SQLite;
using System.Data.SQLite;
using PFT.Base;
using PFT.Interfaces;

namespace PFT.Data
{
    public class Transaction_Tags_ColData : ITransaction_Tags_ColData
    {
        public Transaction_Tag_Col LoadAll()
        {
            Transaction_Tag_Col returnCol = new Transaction_Tag_Col();

            try
            {
                //SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Transaction_Tags";

                //SQLiteDataReader reader = cmd.ExecuteReader();
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Transaction_Tag transaction_tag = new Transaction_Tag();
                    transaction_tag.TransactionId = int.Parse(reader["TransactionId"].ToString());
                    transaction_tag.TagId = int.Parse(reader["TagId"].ToString());

                    returnCol.Add(transaction_tag);
                }

                return returnCol;

            }
            catch
            {
                throw;
            }
        }

        public Transaction_Tag_Col LoadByTransactionId(int transactionId)
        {
            Transaction_Tag_Col returnCol = new Transaction_Tag_Col();

            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Transaction_Tags WHERE TransactionId = " + transactionId;

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Transaction_Tag transaction_tag = new Transaction_Tag();
                    transaction_tag.TransactionId = int.Parse(reader["TransactionId"].ToString());
                    transaction_tag.TagId = int.Parse(reader["TagId"].ToString());

                    returnCol.Add(transaction_tag);
                }

                return returnCol;

            }
            catch
            {
                throw;
            }
        }

        public Transaction_Tag_Col LoadByTagId(int tagId)
        {
            Transaction_Tag_Col returnCol = new Transaction_Tag_Col();

            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Transaction_Tags WHERE TagId = " + tagId;

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Transaction_Tag transaction_tag = new Transaction_Tag();
                    transaction_tag.TransactionId = int.Parse(reader["TransactionId"].ToString());
                    transaction_tag.TagId = int.Parse(reader["TagId"].ToString());

                    returnCol.Add(transaction_tag);
                }

                return returnCol;

            }
            catch
            {
                throw;
            }
        }



    }
}
