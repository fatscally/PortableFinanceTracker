using PFT.Base;
using System.Data.SqlServerCe;
using PFT.Interfaces;

namespace PFT.Data
{
    public class RepeatTypeColData : IRepeatTypes
    {
        public RepeatTypeCol LoadAll()
        {
            RepeatTypeCol returnCol = new RepeatTypeCol();

            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM RepeatTypes";

                SqlCeDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    RepeatType repeatType = new RepeatType();
                    repeatType.Id = int.Parse(reader["Id"].ToString());
                    repeatType.Name = reader["[Name]"].ToString();

                    returnCol.Add(repeatType);
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
