using PFT.Base;
using System;

namespace PFT.Interfaces
{
    interface IPaymentTypeData
    {
        void Save(PaymentType paymentType);
        void Delete(PaymentType paymentType);

        PaymentType Select(int paymentTypeId);

        double GetPaymentTypeSpendTotal(int supplierId, DateTime startDate, DateTime endDate, bool isIncome);
    }
}
