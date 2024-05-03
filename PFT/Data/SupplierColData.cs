using System.Data.SQLite;
using PFT.Base;
using PFT.Interfaces;

namespace PFT.Data
{
    public class SupplierColData : ISupplierColData
    {


        public SupplierCol LoadAll()
        {
            SupplierCol returnCol = new SupplierCol();

            try
            {
                SQLiteCommand cmd = Globals.Instance.SQLiteConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Suppliers";

                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Supplier supplier = new Supplier();
                    supplier.Id = int.Parse(reader["Id"].ToString());
                    supplier.Name = reader["Name"].ToString();
                    supplier.Description = reader["Description"].ToString();

                    returnCol.Add(supplier);
                }

                return returnCol;

            }
            catch
            {
                //throw;
                return null;
            }
        }







    }
}
