using PFT.Interfaces;
using PFT.Base;
using System.Data.SQLite;

namespace PFT.Data
{
    public class Items_Tags_Data : IItems_Tags_Data
    {
        public void Save(Items_Tags itemsTags)
        {
            //easier then making an If Row Exists
            Delete(itemsTags);

            insert(itemsTags);
        }


        public Items_Tags Select(Items_Tags itemsTags)
        {

            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM ItemDefaultTags WHERE ItemId=" + itemsTags.ItemId + " AND TagId=" + itemsTags.TagId;

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    itemsTags.ItemId = int.Parse(reader["ItemId"].ToString());
                    itemsTags.TagId = int.Parse(reader["TagId"].ToString());
                }

                return itemsTags;
            }
            catch
            {
                throw;
            }
        }

        private void insert(Items_Tags itemsTags)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO ItemDefaultTags (ItemId, TagId) Values('" + itemsTags.ItemId + "', '" + itemsTags.TagId + "')";

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }
        public void Delete(Items_Tags itemsTags)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "DELETE FROM ItemDefaultTags WHERE ItemId=" + itemsTags.ItemId + " AND TagId=" + itemsTags.TagId;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }

    }
}
