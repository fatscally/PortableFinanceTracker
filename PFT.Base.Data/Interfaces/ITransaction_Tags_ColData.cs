using PFT.Base;

namespace PFT.Interfaces
{
    interface ITransaction_Tags_ColData
    {
        Transaction_Tag_Col LoadAll();
        Transaction_Tag_Col LoadByTransactionId(int transactionId);
        Transaction_Tag_Col LoadByTagId(int tagId);
    }
}
