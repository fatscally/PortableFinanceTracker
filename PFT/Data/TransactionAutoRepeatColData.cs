using System;
using System.Data.SqlServerCe;
using PFT.Base;
using PFT.Interfaces;


namespace PFT.Data
{
    internal class TransactionAutoRepeatColData
    {
        public TransactionAutoRepeatCol LoadAll()
        {
            TransactionAutoRepeatCol returnCol = new TransactionAutoRepeatCol();

            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Transactions";

                SqlCeDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    TransactionAutoRepeat transaction = new TransactionAutoRepeat();
                    transaction.Id = int.Parse(reader["Id"].ToString());
                    transaction.ItemId = int.Parse(reader["ItemId"].ToString());
                    transaction.Price = decimal.Parse(reader["Price"].ToString());
                    transaction.Comment = reader["[Comment]"].ToString();
                    transaction.SupplierId = int.Parse(reader["SupplierId"].ToString());
                    transaction.RepeatTypeId = int.Parse(reader["RepeatTypeId"].ToString());
                    transaction.RepeatDay = int.Parse(reader["RepeatDay"].ToString());

                    returnCol.Add(transaction);
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
