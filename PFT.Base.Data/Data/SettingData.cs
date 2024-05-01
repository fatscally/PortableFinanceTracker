using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SQLite;
using PFT.Base;
using PFT.Interfaces;

namespace PFT.Data
{
    public class SettingData : ISettingData
    {
        public void Save(Setting setting)
        {
            string settingKey = setting.Key;

            //"If Exists" isn't used in SqlCe so I have to do a separate SELECT
            settingKey = selectByKey(setting.Key).Key;

            //if it is still <=0 after the select then INSERT
            if (settingKey == null)
                insert(setting);
            else
                Update(setting);
        }


        private Setting selectByKey(string key)
        {

            Setting setting = new Setting();

            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Settings WHERE [Key]='" + key + "'";

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    setting.Id = int.Parse(reader["Id"].ToString());
                    setting.Key = reader["Key"].ToString();
                    setting.Value = reader["Value"].ToString();
                }

                return setting;
            }
            catch
            {
                throw;
            }


        }


        private void insert(Setting setting)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "INSERT INTO Settings ([Key], [Value]) Values('" + setting.Key + "', '" + setting.Value + "')";

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        public void Update(Setting setting)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "UPDATE Settings Set [Key]='" + setting.Key + "', [Value]='" + setting.Value + "'  WHERE [Key] = '" + setting.Key + "'";

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }


        public void Delete(Setting setting)
        {
            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "DELETE FROM Settings WHERE [Key] = '" + setting.Key + "'";

                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
        }
    }
}
