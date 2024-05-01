using PFT.Base;

namespace PFT.Interfaces
{
    interface ITransactionAutoRepeat
    {
        void Save(TransactionAutoRepeat transactionAutoRepeat);
        void Delete(TransactionAutoRepeat transactionAutoRepeat);
    }
}
