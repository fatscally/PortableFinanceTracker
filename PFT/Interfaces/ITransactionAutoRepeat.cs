using PFT.Base;
using System;

namespace PFT.Interfaces
{
    interface ITransactionAutoRepeat
    {
        TransactionAutoRepeatCol LoadAutoInsertsToday();
    }
}
