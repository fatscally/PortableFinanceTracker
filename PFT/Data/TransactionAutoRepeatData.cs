using System.Data.SQLite;
using PFT.Base;
using PFT.Interfaces;

namespace PFT.Data
{
    internal class TransactionAutoRepeatData : ITransactionAutoRepeat
    {


        public TransactionAutoRepeatCol LoadAutoInsertsToday()
        {

            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM TransactionAutoRepeat";

                cmd.ExecuteReader();
            }
            catch
            {
                throw;
            }

            return null;

        }

        public void Save(TransactionAutoRepeat transaction)
        {
            int transactionId = transaction.Id;

            if (transactionId <= 0)
                transactionId = Select(transaction).Id;

        }

        public Transaction Select(TransactionAutoRepeat transaction)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT TOP(1) FROM TransactionAutoRepeat WHERE Id=" + transaction.Id.ToString();

                cmd.ExecuteReader();
            }
            finally
            {
                //TODO Delete this and only close when the app exits
                Globals.Instance.SQLiteConnection.LocalConnection().Close();
            }

            return null;

        }




        private void insert(TransactionAutoRepeat transaction)
        {

        }


        private void update(TransactionAutoRepeat transaction)
        {

        }

        public void Delete(TransactionAutoRepeat transaction)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "DELETE FROM TransactionAutoRepeat WHERE Id = " + transaction.Id;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }




    }
}
