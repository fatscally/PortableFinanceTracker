using System;
using PFT.Base;
using PFT.Interfaces;
using System.Data.SqlServerCe;

namespace PFT.Data
{
    public class SupplierColData : ISupplierColData
    {


        public SupplierCol LoadAll()
        {
            SupplierCol returnCol = new SupplierCol();

            try
            {
                SqlCeCommand cmd = Globals.Instance.SqlCeConnection.LocalConnection().CreateCommand();
                cmd.CommandText = "SELECT * FROM Suppliers";

                SqlCeDataReader reader = cmd.ExecuteReader();

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
