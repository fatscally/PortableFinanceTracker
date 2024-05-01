using System;
using PFT.Data;
using PFT.Pages;

namespace PFT
{
    public sealed class Globals
    {
        private Globals()
        {}

        public static Globals Instance { get { return lazy.Value; } }

        private static readonly Lazy<Globals> lazy = new Lazy<Globals>(() => new Globals());

        private Settings _settings;

        public Settings Settings
        {
            get {
                if (_settings == null)
                    _settings = new Settings();
                
                return _settings;
            }
            set { _settings = value; }
        }

        private Connection _SQLiteConnection;

        public Connection SQLiteConnection
        {
            get {
                if (_SQLiteConnection == null)
                    _SQLiteConnection = new Connection();

                return _SQLiteConnection; }
            set { _SQLiteConnection = value; }
        }

                

    }
}
