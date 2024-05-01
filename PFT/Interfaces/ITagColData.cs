using PFT.Base;

namespace PFT.Interfaces
{
    interface ITagColData
    {
        TagCol LoadAll();
        TagCol LoadWithTagsByTransactionId(int transactionId);
        void SaveAllTransaction_Tags(int transactionId, TagCol tagCol);
    }
}
