using System;
using System.Data.SQLite;



//Handle everything for the database connections in here

namespace PFT.Data
{
    public class Connection
    {

        private SQLiteConnection _localConnection;

        public SQLiteConnection LocalConnection()
        {

            if (_localConnection == null)
                _localConnection = new SQLiteConnection();

            try
            {
                if (_localConnection.State != System.Data.ConnectionState.Open)
                {
       
                    _localConnection = new SQLiteConnection("Data Source = ..\\..\\PFTSqlite;");
                    _localConnection.Open();
                }

                return _localConnection;

            }
            catch (Exception)
            {
                throw;
            }

        }

        //private SQLiteConnection _localConnection;

        //public SQLiteConnection LocalConnection()
        //{

        //    if (_localConnection == null)
        //        _localConnection = new SQLiteConnection();

        //    try
        //    {
        //        if (_localConnection.State != System.Data.ConnectionState.Open)
        //        {
        //            //Data Source=C:\My Projects\Ray\Tiny_WPF_db\Tiny_WPF_db\Tiny.sdf
        //            //Data Source=C:\My Projects\Ray\Tiny_WPF_db\Tiny_WPF_db\bin\Debug\Tiny.sdf

        //            _localConnection = new SQLiteConnection("Data Source = ..\\..\\PFT.sdf;");
        //            _localConnection.Open();
        //        }

        //        return _localConnection;

        //    }
        //    catch (Exception)
        //    {
        //        throw;
        //    }

        //}




        

    }
}
