using PFT.Base;

namespace PFT.Interfaces
{
    public interface ITagColData
    {
        TagCol LoadAll();
        TagCol LoadWithTagsByTransactionId(int transactionId);
        void SaveAllTransaction_Tags(int transactionId, TagCol tagCol);
    }
}
