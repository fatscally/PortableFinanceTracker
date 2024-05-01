using PFT.Base;

namespace PFT.Interfaces
{
    interface ISettingData
    {
        void Save(Setting setting);
        void Update(Setting setting);
        void Delete(Setting setting);
    }
}
