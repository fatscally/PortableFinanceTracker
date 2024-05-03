using PFT.Base;
using System;

namespace PFT.Interfaces
{
    public interface IPaymentTypeData
    {
        void Save(PaymentType paymentType);
        void Delete(PaymentType paymentType);

        PaymentType Select(int paymentTypeId);

        double GetPaymentTypeTotalSpend(int supplierId, DateTime startDate, DateTime endDate, bool isIncome);
    }
}
