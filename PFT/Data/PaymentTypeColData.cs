using System;
using PFT.Base;
using PFT.Interfaces;
using System.Data.SqlServerCe;

namespace PFT.Data
{
    public class PaymentTypeColData : IPaymentTypeColData
    {


        public PaymentTypeCol LoadAll()
        {
            PaymentTypeCol returnCol = new PaymentTypeCol();

            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM PaymentTypes";

                SqlCeDataReader reader = cmd.ExecuteReader();

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
