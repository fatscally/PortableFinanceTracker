using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFT.Base;
using PFT.Interfaces;
using System.Data.SqlServerCe;

namespace PFT.Data
{
    internal class TransactionAutoRepeatData : ITransactionAutoRepeat
    {




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
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT TOP(1) FROM TransactionAutoRepeat WHERE Id=" + transaction.Id.ToString();

                cmd.ExecuteReader();
            }
            finally
            {
                //TODO Delete this and only close when the app exits
                Globals.Instance.SqlCeConnection.LocalConnection().Close();
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
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
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
