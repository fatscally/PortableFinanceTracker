using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PFT.Base;
using PFT.Interfaces;
using System.Data.SqlServerCe;

namespace PFT.Data
{
    public class SettingColData : ISettingColData
    {
        public SettingCol Load()
        {
            SettingCol settingColReturn = new SettingCol();

            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Settings";

                SqlCeDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Setting setting = new Setting();
                    setting.Id = int.Parse(reader["Id"].ToString());
                    setting.Key = reader["Key"].ToString();
                    setting.Value = reader["Value"].ToString();

                    settingColReturn.Add(setting);
                }

                return settingColReturn;

            }
            catch
            {
                throw;
            }
        }

        public void Save(SettingCol settingCol)
        {
            new NotImplementedException();
        }
    }
}
