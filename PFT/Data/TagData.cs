using PFT.Base;
using System.Data.SQLite;
using PFT.Interfaces;
using System;

namespace PFT.Data
{
    public class TagData : ITagData
    {

        public void Save(Tag tag)
        {
            int tagId = tag.Id;

            //"If Exists" isn't used in SqlCe so I have to do a separate SELECT
            //if (tagId <= 0)
            //    tagId = select(tag).Id;
            
            //if it is still <=0 after the select then INSERT
            if (tagId <= 0)
                insert(tag);
            else
                Update(tag);
         }


        private Tag select(Tag tag)
        {

            if (tag.Name == "")
                return tag;

            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Tags WHERE Id=" + tag.Id;
                
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tag.Id = int.Parse(reader["Id"].ToString());
                    tag.Name = reader["Name"].ToString();
                    tag.Description = reader["Description"].ToString();
                    tag.Budget = float.Parse(reader["Budget"].ToString());
                    tag.ParentTagId = int.Parse(reader["ParentTagId"].ToString());
                }

                return tag;
            }
            catch
            {
                throw;
            }


        }
        

        private void insert(Tag tag)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO Tags (Name, Description, ParentTagId, Budget) Values('" + tag.Name + "', '" + tag.Description + "', '" + tag.ParentTagId + "', '" + tag.Budget + "')";

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        private void Update(Tag tag)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "UPDATE Tags Set Name='" + tag.Name + "', Description='" + tag.Description + "', ParentTagId='" + tag.ParentTagId + "', Budget='" + tag.Budget + "'  WHERE Id = " + tag.Id;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        public void Delete(Tag tag)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "DELETE FROM Tags WHERE Id = " + tag.Id;

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
        public double GetTagTotalSpend(int tagId, DateTime startDate, DateTime endDate, bool isIncome)
        {
            TransactionCol returnCol = new TransactionCol();
            string strCmdText;


            //string strIsIncome = "0";  //stupid SQLCE bug won't handle True/False
            //if (isIncome)
            //    strIsIncome = "1";


            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();

                strCmdText = "SELECT SUM(Transactions.Price) AS TotalSpend ";
                strCmdText += "FROM Transaction_Tags INNER JOIN Transactions ON Transaction_Tags.TransactionId = Transactions.Id ";
                strCmdText += "WHERE Transactions.IsIncome = 0 AND (Transactions.Date >= '" + startDate.ToString("yyyy/MM/dd") + " 00:00:00') AND (Transactions.Date <= '" + endDate.ToString("yyyy/MM/dd ") + " 23:59:59') AND Transaction_Tags.TagId = " + tagId.ToString();


                cmd.CommandText = strCmdText;
                object rtnVal = cmd.ExecuteScalar();

                double outNumber;
                double.TryParse(rtnVal.ToString(), out outNumber);

                return outNumber;

            }
            catch
            {
                throw;
                //return 0;
            }
        }


    }
}
