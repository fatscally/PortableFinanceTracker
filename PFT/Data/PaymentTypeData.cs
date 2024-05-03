using PFT.Base;
using System.Data.SQLite;
using PFT.Interfaces;
using System;

namespace PFT.Data
{
    public class PaymentTypeData : IPaymentTypeData
    {
        public void Save(PaymentType paymentType)
        {

            //"If Exists" isn't used in SqlCe so I have to do a separate SELECT
            //if (paymentType.Id <= 0)
            //    paymentType = Select(paymentType.Id);

            //if it is still <=0 after the select then INSERT
            if (paymentType.Id <= 0)
                insert(paymentType);
            else
                Update(paymentType);
        }

        public PaymentType Select(PaymentType paymentType)
        {
            if (paymentType.Name == "")
                return paymentType;

            return Select(paymentType.Id);
        }


        public PaymentType Select(int paymentTypeId)
        {
            try
            {
                PaymentType paymentType = new PaymentType();

                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM PaymentTypes WHERE Id=" + paymentTypeId;

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    paymentType.Id = int.Parse(reader["Id"].ToString());
                    paymentType.Name = reader["Name"].ToString();
                    paymentType.Description = reader["Description"].ToString();
                }

                return paymentType;
            }
            catch
            {
                throw;
            }


        }

        private void insert(PaymentType paymentType)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO PaymentTypes (Name, Description) Values('" + paymentType.Name + "', '" + paymentType.Description + "')";

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        private void Update(PaymentType paymentType)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "UPDATE PaymentTypes Set Name='" + paymentType.Name + "', Description='" + paymentType.Description + "'  WHERE Id = " + paymentType.Id;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

        public void Delete(PaymentType paymentType)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "DELETE FROM PaymentTypes WHERE Id = " + paymentType.Id;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        /// <summary>
        /// Gets the total of all money spent with the Payment type for the given date range.
        /// </summary>
        /// <param name="startDate">Start date of period to calculate</param>
        /// <param name="endDate">End date of period to calculate</param>
        /// /// <param name="isIncome">True if calculating incomes</param>
        /// <returns>Sum of money spent </returns>
        public double GetPaymentTypeTotalSpend(int paymentTypeId, DateTime startDate, DateTime endDate, bool isIncome)
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
                strCmdText += "WHERE PaymentTypeId = " + paymentTypeId + " AND IsIncome = " + strIsIncome;
                strCmdText += " AND Date Between '" + startDate.ToString("yyyy-MM-dd") + " 00:00:00:001' AND '" + endDate.ToString("yyyy-MM-dd ") + "23:59:59'";



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
