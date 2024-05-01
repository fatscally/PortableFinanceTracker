using PFT.Base;

namespace PFT.Interfaces
{
    interface ITransaction_Tags_Data
    {
        void Save(Transaction_Tag transaction_tag);
        void Delete(Transaction_Tag transaction_tag);
        void DeleteTagsByTransactionId(int transactionId);
    }
}
