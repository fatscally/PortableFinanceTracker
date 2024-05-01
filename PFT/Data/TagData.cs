using PFT.Base;
using System.Data.SqlServerCe;
using PFT.Interfaces;

namespace PFT.Data
{
    public class TagData : ITagData
    {

       
      
        public void Save(Tag tag)
        {
            int tagId = tag.Id;

            //"If Exists" isn't used in SqlCe so I have to do a separate SELECT
            if (tagId <= 0)
                tagId = select(tag).Id;
            
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
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Tags WHERE Id=" + tag.Id;
                
                SqlCeDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    tag.Id = int.Parse(reader["Id"].ToString());
                    tag.Name = reader["Name"].ToString();
                    tag.Description = reader["Description"].ToString();
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
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO Tags (Name, Description, ParentTagId) Values('" + tag.Name + "', '" + tag.Description + "', '" + tag.ParentTagId + "')";

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
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "UPDATE Tags Set Name='" + tag.Name + "', Description='" + tag.Description + "', ParentTagId='" + tag.ParentTagId + "'  WHERE Id = " + tag.Id;

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
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "DELETE FROM Tags WHERE Id = " + tag.Id;

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


    }
}
