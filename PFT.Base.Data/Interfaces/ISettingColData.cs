using PFT.Base;

namespace PFT.Interfaces
{
    public interface ISettingColData
    {
        SettingCol Load();
        void Save(SettingCol settingCol);
    }
}
