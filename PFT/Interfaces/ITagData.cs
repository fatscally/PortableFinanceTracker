using PFT.Base;
using System;

namespace PFT.Interfaces
{
    public interface ITagData
    {
        void Save(Tag tag);
        //void Update(Tag tag);
        void Delete(Tag tag);

        double GetTagTotalSpend(int supplierId, DateTime startDate, DateTime endDate, bool isIncome);

    }
}
