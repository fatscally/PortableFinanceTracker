using System.Data.SqlServerCe;
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
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Items";

                SqlCeDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Item item = new Item();
                    item.Id = int.Parse(reader["Id"].ToString());
                    item.Name = reader["Name"].ToString();
                    item.Description = reader["Description"].ToString();
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
