using System;
using System.Data.SqlServerCe;


//Handle everything for the database connections in here

namespace PFT.Data
{
    public class Connection
    {
        private SqlCeConnection _localConnection;

        public SqlCeConnection LocalConnection()
        {

            if (_localConnection == null)
                _localConnection = new SqlCeConnection();

            try
            {
                if (_localConnection.State != System.Data.ConnectionState.Open)
                {
                    //Data Source=C:\My Projects\Ray\Tiny_WPF_db\Tiny_WPF_db\Tiny.sdf
                    //Data Source=C:\My Projects\Ray\Tiny_WPF_db\Tiny_WPF_db\bin\Debug\Tiny.sdf

                    _localConnection = new SqlCeConnection("Data Source = ..\\..\\PFT.sdf;");
                    _localConnection.Open();
                }

                return _localConnection;

            }
            catch (Exception)
            {
                throw;
            }

        }


        

    }
}
