using System;
using PFT.Base;
using PFT.Interfaces;
using System.Data.SQLite;

namespace PFT.Data
{
    public class PaymentTypeColData : IPaymentTypeColData
    {


        public PaymentTypeCol LoadAll()
        {
            PaymentTypeCol returnCol = new PaymentTypeCol();

            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM PaymentTypes";

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    PaymentType paymentType = new PaymentType();
                    paymentType.Id = int.Parse(reader["Id"].ToString());
                    paymentType.Name = reader["Name"].ToString();
                    paymentType.Description = reader["Description"].ToString();

                    returnCol.Add(paymentType);
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
