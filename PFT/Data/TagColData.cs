using System;
using PFT.Base;
using PFT.Interfaces;
using System.Data.SqlServerCe;

namespace PFT.Data
{
    public class TagColData : ITagColData
    {

        public TagCol LoadAll()
        {
            TagCol returnCol = new TagCol();

            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Tags";

                SqlCeDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Tag tag = new Tag();
                    tag.Id = reader.GetInt32(0); //Id
                    tag.Name = reader["Name"].ToString();
                    tag.Description = reader["Description"].ToString();
                    tag.ParentTagId = reader.GetInt32(3); //ParentTagId
                    
                    
                    returnCol.Add(tag);
                }

                return returnCol;
                
            }
            catch
            {
                throw;
            }
        }

        public TagCol LoadWithTagsByTransactionId(int transactionId)
        {

            //Get the list of tags for this transaction from Transaction_Tags
            ITransaction_Tags_ColData ttcd = new Transaction_Tags_ColData();
            Transaction_Tag_Col ttc = new Transaction_Tag_Col();
            ttc = ttcd.LoadByTransactionId(transactionId);


            TagCol returnCol = new TagCol();

            try
            {
                foreach (Transaction_Tag tt in ttc)
                {

                    SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                    cmd.CommandText = "SELECT * FROM Tags WHERE Id=" + tt.TagId;

                    SqlCeDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        Tag tag = new Tag();
                        tag.Id = reader.GetInt32(0); //Id
                        tag.Name = reader["Name"].ToString();
                        tag.Description = reader["Description"].ToString();
                        tag.ParentTagId = reader.GetInt32(3); //ParentTagId
                        tag.IsChecked = true; //IsChecked

                        returnCol.Add(tag);
                    }
                }

                return returnCol;

            }
            catch
            {
                throw;
            }
        }

        public void SaveAllTransaction_Tags(int transactionId, TagCol tagCol)
        {
            Transaction_Tag tt = new Transaction_Tag();
            ITransaction_Tags_Data ttd = new Transaction_Tags_Data();
            
            //clean out any previous tags on this Transaction.
            ttd.DeleteTagsByTransactionId(transactionId);

            foreach (Tag tag in tagCol)
            {
                tt.TransactionId = transactionId;
                tt.TagId = tag.Id;
                ttd.Save(tt);
            }

        }

    }
}
