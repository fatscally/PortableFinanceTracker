using System;
using PFT.Base;

namespace PFT.Interfaces
{
    public interface IItemData
    {
        void Save(Item item);
        //void Update(Item item);
        void Delete(Item item);

        Item Select(int itemId);

        double GetItemTotalSpend(int id, DateTime startDate, DateTime endDate, bool isIncome);

    }
}
