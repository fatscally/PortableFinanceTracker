using PFT.Base;
using System.Data.SQLite;
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
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM RepeatTypes";

                SQLiteDataReader reader = cmd.ExecuteReader();

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
