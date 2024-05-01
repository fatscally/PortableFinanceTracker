using PFT.Base;
using System;

namespace PFT.Interfaces
{
    interface ISupplierData
    {
        void Save(Supplier supplier);
        void Delete(Supplier supplier);
        Supplier Select(int supplierId);

        double GetSupplierSpendTotal(int supplierId, DateTime startDate, DateTime endDate, bool isIncome);

    }
}
