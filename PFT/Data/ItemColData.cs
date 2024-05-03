using System.Data.SQLite;
using System.Data.SQLite.Linq;

using PFT.Base;
using PFT.Interfaces;

namespace PFT.Data
{
    public class ItemColData : IItemColData
    {
        public ItemCol LoadAll()
        {
            ItemCol returnCol = new ItemCol();

            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Items";

                SQLiteDataReader reader = cmd.ExecuteReader();

                 

                while (reader.Read())
                {
                    Item item = new Item();
                    item.Id = int.Parse(reader["Id"].ToString());
                    item.Name = reader["Name"].ToString();
                    item.Description = reader["Description"].ToString();
                    item.Budget = float.Parse(reader["Budget"].ToString());
                    //item.NeedIt = bool.Parse(reader["NeedIt"].ToString());
                    item.DefaultPrice = float.Parse(reader["DefaultPrice"].ToString());

                    returnCol.Add(item);
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
