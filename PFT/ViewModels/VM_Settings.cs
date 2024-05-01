using System.Windows.Input;
using PFT.Base;
using PFT.Data;
using PFT.Interfaces;
using PFT.Utils;

namespace PFT.ViewModels
{
    public partial class VM_Main
    {
        private SettingCol _settings;

        public SettingCol Settings
        {
            get
            {
                if (_settings == null)
                    _settings = new SettingCol();

                return _settings;
            }
            set { _settings = value; }
        }

        private Setting _currentSetting;

        public Setting CurrentSetting
        {
            get
            {
                if (_currentSetting == null)
                    _currentSetting = new Setting();

                return _currentSetting;
            }
            set { _currentSetting = value; }
        }

        private ICommand _saveSetting_ClickCommand;
        public ICommand SaveSetting_ClickCommand
        {
            get
            {
                return _saveSetting_ClickCommand ?? (_saveSetting_ClickCommand = new CommandHandler(() => SaveSetting(CurrentSetting), _canExecute));
            }
        }
        public void SaveSetting(Setting setting)
        {
            ISettingData sd = new SettingData();
            sd.Save(setting);
            GetAllSettings();
            System.Windows.MessageBox.Show("Saved");
        }

        private void GetAllSettings()
        {
            try
            {
                ISettingColData iscd = new SettingColData();
                Settings.Clear();
                foreach (Setting s in iscd.Load())
                    Settings.Add(s);
            }
            catch (System.Exception)
            {

                throw;
            }

        }
    }
}
