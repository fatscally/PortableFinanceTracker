using PFT.Base;
using System;

namespace PFT.Interfaces
{
    public interface ITransactionAutoRepeatColData
    {
        TransactionCol LoadAll(DateTime startDate, DateTime endDate);
        double GetTransactionBalance(DateTime startDate, DateTime endDate, bool isIncome);
    }
}
